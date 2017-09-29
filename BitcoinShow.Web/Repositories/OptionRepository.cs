using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BitcoinShow.Web.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly BitcoinShowDBContext _context;
        public OptionRepository(BitcoinShowDBContext context)
        {
            _context = context;
        }
        public void Add(Option newOption)
        {
            if (string.IsNullOrEmpty(newOption.Text))
            {
                throw new ArgumentNullException(nameof(newOption.Text));
            }

            List<PropertyInfo> props = typeof(Option)
                .GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(MaxLengthAttribute), true).Any()).ToList();

            MaxLengthAttribute max = props.First(p => p.Name == nameof(newOption.Text))
                .GetCustomAttributes(true)
                    .Where(attr => attr.GetType() == typeof(MaxLengthAttribute))
                    .FirstOrDefault() as MaxLengthAttribute;
            if (max != null)
            {
                if (max.Length < newOption.Text.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(newOption.Text));
                }
            }

            _context.Options.Add(newOption);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var optionToRemove = _context.Options.Find(id);
            if (optionToRemove == null)
            {
                throw new DbUpdateException("The current option does not exists.", new NullReferenceException());
            }
            _context.Options.Remove(optionToRemove);
            _context.SaveChanges();
        }

        public Option Get(int id)
        {
            return _context.Options.Find(id);
        }

        public void Update(Option option)
        {
            if (string.IsNullOrEmpty(option.Text))
            {
                throw new ArgumentNullException(nameof(option.Text));
            }

            List<PropertyInfo> props = typeof(Option)
                .GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(MaxLengthAttribute), true).Any()).ToList();

            MaxLengthAttribute max = props.First(p => p.Name == nameof(option.Text))
                .GetCustomAttributes(true)
                    .Where(attr => attr.GetType() == typeof(MaxLengthAttribute))
                    .FirstOrDefault() as MaxLengthAttribute;
            if (max != null)
            {
                if (max.Length < option.Text.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(option.Text));
                }
            }
            var actualOption = _context.Options.Find(option.Id);
            if (actualOption == null)
            {
                throw new DbUpdateException("The current option does not exists.", new NullReferenceException());
            }
            else
            {
                actualOption.Text = option.Text;
                _context.Options.Update(actualOption);
                _context.SaveChanges();
                option = actualOption;
            }
        }
    }
}