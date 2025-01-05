namespace MangaReader.Model
{
    internal class Bookmark
    {
        public int ChapterNumber { get; set; }
        public int PageNumber { get; set; }

        public Bookmark() : this(0, 0) { }

        public Bookmark(int chapterNumber, int pageNumber)
        {
            ChapterNumber = chapterNumber;
            PageNumber = pageNumber;
        }
    }
}
