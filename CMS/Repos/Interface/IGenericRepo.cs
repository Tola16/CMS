namespace CMS.Repos.Interface
{
    public interface IGenericRepo<T> where T : class
    {
        public T GetById(int id);
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public List<T> GetAll();
    }
}
