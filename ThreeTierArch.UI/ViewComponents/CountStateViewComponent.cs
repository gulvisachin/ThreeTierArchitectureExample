using Microsoft.AspNetCore.Mvc;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.UI.ViewComponents
{
    [ViewComponent(Name = "CountState")]
    public class CountStateViewComponent : ViewComponent
    {
        private IState _stateRepo;

        public CountStateViewComponent(IState stateRepo)
        {
            _stateRepo = stateRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var states = await _stateRepo.GetAllAsych();
            return View(states.Count());

        }
    }
}
