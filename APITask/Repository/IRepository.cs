namespace APITask.Repository
{
    public interface IRepository<T>
    {
        string ServiceId { get; set; }
        List<T> GetAll(string includes = null);
        T Get(int id);
        void Update(T obj);
        void Insert(T obj);
        void Delete(int id);
        void Save();
    }
}
