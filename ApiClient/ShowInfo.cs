namespace TvMaze.ApiClient
{
    public class ShowInfo
    {
        public int Id { get; }

        public string Name { get; }        

        public ShowInfo(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
