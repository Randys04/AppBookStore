using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppBookStore.Repositories.Abstract
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imgFile);
        public bool DeleteImage(string imgFileName);
    }
}