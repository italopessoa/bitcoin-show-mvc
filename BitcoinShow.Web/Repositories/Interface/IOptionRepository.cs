using BitcoinShow.Web.Models;

namespace BitcoinShow.Web.Repositories.Interface
{
    public interface IOptionRepository
    {
        /// <summary>
        ///     Add new Option. The result will be store into the current param.
        /// </summary>
        /// <param name="newOption"> Option object. </param>
        /// <returns> Option <see cref="Option" /> </returns>
        void Add(Option newOption);

        /// <summary>
        ///     Search option by id.
        /// </summary>
        /// <param name="id"> Option Id. </param>
        /// <returns> Option. </returns>
        Option Get(int id);

        /// <summary>
        ///     Delete option.
        /// </summary>
        /// <param name="id"> Option Id. </param>
        void Delete(int id);

        /// <summary>
        ///     Update option.
        /// </summary>
        /// <param name="option"> Option object with new values. </param>
        void Update(Option option);
    }
}