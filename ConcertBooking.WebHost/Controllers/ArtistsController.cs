using AutoMapper;
using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.WebHost.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcertBooking.WebHost.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ArtistsController : Controller
    {
        private readonly IArtist _artistRepo;
        private readonly IUtility _utilityRepo;
        private IMapper _mapper;
        private string ContainerName = "ArtistImage";
        [BindProperty]
        public ArtistVM artistVM { get; set; }
        //[BindProperty]
        CreateArtistVM createArtistVM = new CreateArtistVM();
        public ArtistsController(IArtist artistRepo, IUtility utilityRepo, IMapper mapper)
        {
            _artistRepo = artistRepo;
            _utilityRepo = utilityRepo;
            _mapper = mapper;

            artistVM = new ArtistVM()
            {
                artist = new Artist(),
                artists = new List<Artist>()
            };
            //createArtistVM = new CreateArtistVM()
            //{
            //    Id = 0,
            //    Name = "",
            //    Bio = "",
            //    ImageUrl = null,
            //};
        }

        public async Task<IActionResult> AllArtists()
        {
            var lstArtists = await _artistRepo.GetAllAsych();
            return Json(new { data = lstArtists ?? null });
        }
        public async Task<IActionResult> Index()
        {
            var lstArtists = await _artistRepo.GetAllAsych();
            return View(lstArtists);
        }

        public async Task<IActionResult> CreateUpdate(int id)
        {
            if (id != 0)
            {
                artistVM.artist = await _artistRepo.GetByIdAsych(id);
                if (artistVM.artist == null || artistVM.artist.Id == 0) return NotFound();

                //createArtistVM = new CreateArtistVM
                //{
                //    Id = artistVM.artist.Id,
                //    Name = artistVM.artist.Name,
                //    Bio = artistVM.artist.Bio,
                //    strImageUrl = artistVM.artist.ImageUrl,
                //};
                createArtistVM = _mapper.Map<CreateArtistVM>(artistVM.artist);

            }
            return View(createArtistVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdate(CreateArtistVM createArtistVM)
        {
            if (ModelState.IsValid)
            {
                if (createArtistVM != null && createArtistVM.Id > 0)
                {
                    var artistById = await _artistRepo.GetByIdAsych(createArtistVM.Id);
                    if (artistById == null) return NotFound();

                    //var artist = BindToView(createArtistVM);
                    var artist = _mapper.Map<Artist>(createArtistVM); 

                    if (createArtistVM.ImageUrl != null)
                    {
                        artist.ImageUrl = await _utilityRepo.EditImage(ContainerName,createArtistVM.ImageUrl,artistById.ImageUrl);
                    }
                    else
                    {
                        artist.ImageUrl = artistById.ImageUrl;
                    }

                    await _artistRepo.Update(artist);
                    TempData["Success"] = "Artist Updated done !";
                }
                else
                {

                    //var artist = BindToView(createArtistVM);
                    var artist = _mapper.Map<Artist>(createArtistVM);

                    if (createArtistVM.ImageUrl != null)
                    {
                        artist.ImageUrl = await _utilityRepo.SaveImage(ContainerName,createArtistVM.ImageUrl);
                    }

                    await _artistRepo.Add(artist);
                    TempData["Success"] = "Artist inserted done !";
                }
            }
            else
            {
                TempData["Error"] = "Something went wrong !";
                return View(artistVM);
            }

            return RedirectToAction(nameof(Index));
        }

        #region DeleteAPICall
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var artistById = await _artistRepo.GetByIdAsych(id);
            if (artistById == null) return Json(new { success = false, error = "Error in fetching data" });
            else
            {
                await _artistRepo.Delete(artistById);
                await _utilityRepo.DeleteImage(ContainerName,artistById.ImageUrl);

                return Json(new { success = true, message = "Artist deleted done." });
            }
        }
        #endregion

        //private Artist BindToView(CreateArtistVM createArtistVM)
        //{
        //    var model = new Artist
        //    {
        //        Id = createArtistVM.Id,
        //        Name = createArtistVM.Name,
        //        Bio = createArtistVM.Bio,
        //    };
        //    var result = _mapper.Map<Artist>(createArtistVM);
        //    return model;
        //}
    }
}