namespace BitcoinShow.Web.Services.Interface
{
    public interface IOptionService
    {
        /// <summary>
        ///     Add new Option. The result will be store into the current param.
        /// </summary>
        /// <param name="text"> Option text. </param>
        /// <returns> Option <see cref="Option" /> </returns>
        object Add(string text);

        /// <summary>
        ///     Search option by id.
        /// </summary>
        /// <param name="id"> Option Id. </param>
        /// <returns> Option. </returns>
        object Get(int id);

        /// <summary>
        ///     Update option.
        /// </summary>
        /// <param name="option"> Option object with new values. </param>
        /// <returns> Option updated. </returns>
        object Update(object option);

        /// <summary>
        ///     Delete option.
        /// </summary>
        /// <param name="id"> Option Id. </param>
        void Delete(int id);
    }
}