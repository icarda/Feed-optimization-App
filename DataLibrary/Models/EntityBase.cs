namespace DataLibrary.Models
{
    public class EntityBase
    {
        protected int _id;
        protected bool _isActive = true;
        public int Id => _id;
        public bool IsActive => _isActive;
    }
}