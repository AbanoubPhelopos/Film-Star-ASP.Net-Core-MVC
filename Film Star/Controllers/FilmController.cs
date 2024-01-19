using Film_Star.Data;
using Film_Star.Models;
using Microsoft.AspNetCore.Mvc;

namespace Film_Star.Controllers
{
	public class FilmController : Controller
	{

		ApplicationDbContext _context;
		IWebHostEnvironment _webHostEnvironment;

		public FilmController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
		{
			_webHostEnvironment = webHostEnvironment;
			_context = context;
		}

		[HttpGet]
		public IActionResult GetIndexView()
		{
			return View("Index", _context.films.ToList());
		}

		[HttpGet]
		public IActionResult GetAFlamView()
		{
			return View("AFlam", _context.films.ToList());
		}

		[HttpGet]
		public IActionResult GetDetailsView(int id)
		{
			Film film = _context.films.FirstOrDefault(e => e.Id == id);

			return View("Details", film);
		}

		[HttpGet]
		public IActionResult GetCreateView()
		{
			//ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "Name");
			return View("Create");
		}

		[HttpPost]
		public IActionResult AddNew(Film film, IFormFile? imageFormFile) // FolanAlfolani.png
		{
			// GUID -> Globally Unique Identifier
			if (imageFormFile != null)
			{
				string imgExtension = Path.GetExtension(imageFormFile.FileName); // .png
				Guid imgGuid = Guid.NewGuid(); // xm789-f07li-624yn-uvx98
				string imgName = imgGuid + imgExtension; // xm789-f07li-624yn-uvx98.png
				string imgUrl = "\\Image\\" + imgName; //  \images\xm789-f07li-624yn-uvx98.png
				film.ImageURL = imgUrl;

				string imgPath = _webHostEnvironment.WebRootPath + imgUrl;

				// FileStream 
				FileStream imgStream = new FileStream(imgPath, FileMode.Create);
				imageFormFile.CopyTo(imgStream);
				imgStream.Dispose();
			}
			else
			{
				film.ImageURL = "\\Image\\No_Image.png";
			}

			if (ModelState.IsValid == true)
			{
				_context.films.Add(film);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				//ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "Name");
				return View("Create");
			}
		}


		[HttpGet]
		public IActionResult GetEditView(int id)
		{
			Film film = _context.films.FirstOrDefault(e => e.Id == id);

			if (film == null)
			{
				return NotFound();
			}
			else
			{
				//ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "Name");
				return View("Edit", film);
			}
		}


		[HttpPost]
		public IActionResult EditCurrent(Film film, IFormFile? imageFormFile)
		{
			// GUID -> Globally Unique Identifier
			if (imageFormFile != null)
			{
				if (film.ImageURL != "\\Image\\No_Image.png")
				{
					string oldImgPath = _webHostEnvironment.WebRootPath + film.ImageURL;

					if (System.IO.File.Exists(oldImgPath) == true)
					{
						System.IO.File.Delete(oldImgPath);
					}
				}


				string imgExtension = Path.GetExtension(imageFormFile.FileName); // .png
				Guid imgGuid = Guid.NewGuid(); // xm789-f07li-624yn-uvx98
				string imgName = imgGuid + imgExtension; // xm789-f07li-624yn-uvx98.png
				string imgUrl = "\\Image\\" + imgName; //  \images\xm789-f07li-624yn-uvx98.png
				film.ImageURL = imgUrl;

				string imgPath = _webHostEnvironment.WebRootPath + imgUrl;

				// FileStream 
				FileStream imgStream = new FileStream(imgPath, FileMode.Create);
				imageFormFile.CopyTo(imgStream);
				imgStream.Dispose();
			}


			if (ModelState.IsValid == true)
			{
				_context.films.Update(film);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				//ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "Name");
				return View("Edit");
			}
		}


		[HttpGet]
		public IActionResult GetDeleteView(int id)
		{
			Film film = _context.films.FirstOrDefault(e => e.Id == id);

			if (film == null)
			{
				return NotFound();
			}
			else
			{
				return View("Delete", film);
			}
		}


		[HttpPost]
		public IActionResult DeleteCurrent(int id)
		{
			Film film = _context.films.FirstOrDefault(e => e.Id == id);
			if (film == null)
			{
				return NotFound();
			}
			else
			{
				if (film.ImageURL != "\\Image\\No_Image.png")
				{
					string imgPath = _webHostEnvironment.WebRootPath + film.ImageURL;

					if (System.IO.File.Exists(imgPath))
					{
						System.IO.File.Delete(imgPath);
					}
				}


				_context.films.Remove(film);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");

			}
		}
	}
}
