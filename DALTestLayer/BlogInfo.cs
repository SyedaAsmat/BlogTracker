namespace DALTestLayer
{
    public class BlogInfo
    {
        public string? Title { get; set; }
        public string? Subject { get; set; }
        public string DateOfCreation { get; set; }
        public string? BlogUrl { get; set; }
        public string? EmpEmailId { get; set; }

        private readonly List<BlogInfo> blogStorage;

        public bool AddBlog(BlogInfo blog)
        {
            try
            {
                blogStorage.Add(blog);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding blog: {ex.Message}");
                return false; 
            }
        }
    }
}
