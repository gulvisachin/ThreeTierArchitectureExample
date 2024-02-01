using PresentationLayer.DataAccessLayer;

namespace PresentationLayer.ViewModels
{
    public class PeopleVM
    {
        public TblPerson People { get; set; } = new TblPerson();
        public IEnumerable<TblPerson> PeopleList { get; set; } = new List<TblPerson>();
    }
}
