using DALTestLayer;

namespace NUnitMoqTest
{
    internal class BlogController
    {
        private BlogInfo @object;

        public BlogController(BlogInfo @object)
        {
            this.@object = @object;
        }

        internal bool? AddNewBlog(BlogInfo blogInfo)
        {
            throw new NotImplementedException();
        }
    }
}