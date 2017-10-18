using System;
using System.Linq;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BitcoinShow.Web.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private BitcoinShowDBContext _context;
        public QuestionRepository(BitcoinShowDBContext context)
        {
            this._context = context;
        }

        public void Add(Question question)
        {
            if(question == null)
            {
                throw new ArgumentNullException("question");
            }
            if(string.IsNullOrEmpty(question.Title))
            {
                throw new ArgumentNullException(nameof(question.Title));
            }
            if(question.Title.Length > 200)
            {
                throw new ArgumentOutOfRangeException(nameof(question.Title), question.Title,"The title has too many characters.");
            }
            if(question.Answer == null)
            {
                throw new ArgumentNullException(nameof(question.Answer), "You must provide Answer navigation property value.");
            }

            this._context.Questions.Add(question);
            this._context.SaveChanges();
        }
        
        public void Delete(int id)
        {
            var question = this._context.Questions.Find(id);
            if(question != null)
            {
                this._context.Questions.Remove(question);
                this._context.SaveChanges();
            }
            else
            {
                throw new Exception("The current Question does not exist.");
            }
        }

        public List<Question> GetAll()
        {
            return this._context.Questions.Include(q =>q.Options).ToList();
        }

        public Question Get(int id)
        {
            return this._context.Questions.Include(q => q.Options).FirstOrDefault(q => q.Id == id);
        }

        public void Update(Question question)
        {
            if(string.IsNullOrEmpty(question.Title))
            {
                throw new ArgumentNullException(nameof(question.Title));
            }
            if(question.Title.Length > 200)
            {
                throw new ArgumentOutOfRangeException(nameof(question.Title), question.Title,"The title has too many characters.");
            }
            if(question.Answer == null)
            {
                throw new ArgumentNullException(nameof(question.Answer), "You must provide Answer navigation property value.");
            }
            this._context.Questions.Update(question);
        }

        public Question GetByLevel(LevelEnum level, int[] excludeIds)
        {
            return _context.Questions.Where(q => !excludeIds.Contains(q.Id) && q.Level == level).Take(1).FirstOrDefault();
        }
    }
}
