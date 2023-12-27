namespace CountLetters
{
    public class FilesName
    {
        public string Name { get; set; }
    }

    //JSON parsing methods
    struct LinkFields
    {
        public String self;
    }
    struct GitFileInfo
    {
        public String name;
        public String type;
        public String download_url;
        public LinkFields _links;
    }

    //Structs used to hold file data
    public struct FileData
    {
        public String name;
        public String contents;
    }
    public struct Directory
    {
        public String name;
        public List<Directory> subDirs;
        public List<FileData> files;
    }
}
