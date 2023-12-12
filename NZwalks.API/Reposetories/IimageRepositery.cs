using NZwalks.API.Models.Domain;
using System.Net;

namespace NZwalks.API.Reposetories
{
    public interface IimageRepositery
    {
        Task<Image>Upload(Image image);
    }
}
