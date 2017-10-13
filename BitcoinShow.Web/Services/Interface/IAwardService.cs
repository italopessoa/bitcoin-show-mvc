using System.Collections.Generic;
using BitcoinShow.Web.Models;

namespace BitcoinShow.Web.Services.Interface
{
    public interface IAwardService
    {
        /// <summary>
        ///     Create a new award.
        /// </summary>
        /// <param name="successValue"> Award success value. </param>
        /// <param name="failValue"> Award fail value. </param>
        /// <param name="quitValue"> Award quit value. </param>
        /// <param name="level"> Award level.</param>
        /// <returns> Award object. </returns>
        Award Add(decimal successValue, decimal failValue, decimal quitValue, LevelEnum level);

        /// <summary>
        ///     Find award by id.
        /// </summary>
        /// <param name="id"> Award id. </param>
        /// <returns> Award object. </returns>
        Award Get(int id);

        /// <summary>
        ///     Get all the awards.
        /// </summary>
        /// <returns> List of Awards. </returns>
        List<Award> GetAll();

        /// <summary>
        ///     Update award values.
        /// </summary>
        /// <param name="award">Award object with values to update. </param>
        void Update(Award award);

        /// <summary>
        ///     Delete award.
        /// </summary>
        /// <param name="id"> Award id. </param>
        void Delete(int id);
    }
}
