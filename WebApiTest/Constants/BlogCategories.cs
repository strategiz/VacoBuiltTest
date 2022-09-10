using WebApiTest.DTO.Posts;

namespace WebApiTest.Constants
{
    public static class BlogCategories
    {
        /// <summary>
        /// This list should be stored in database but in a first time we will hard coded this part
        /// </summary>
        public static List<BlogCategoryInfo> Categories = new List<BlogCategoryInfo>
        {
            new BlogCategoryInfo{Id = 1, Name = "General"},
            new BlogCategoryInfo{Id = 2, Name = "Technology"},
            new BlogCategoryInfo{Id = 3, Name = "Staff"},
            new BlogCategoryInfo{Id = 4, Name = "Random"},
        };
    }
}
