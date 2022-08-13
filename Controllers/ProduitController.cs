
using ApiUser.Entities.Dto;

using Microsoft.AspNetCore.Mvc;
using ApiUser.Services;
using ApiUser.Entities;


/// <summary>
/// This is the produit controller 
/// </summary>


namespace ApiUser.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private IProduitService _produitService;
        private readonly ILogger<UsersController> _logger;

        public ProduitsController(IProduitService produitService, ILogger<UsersController> logger)
        {
            this._produitService = produitService;
            this._logger = logger;
        }


        [HttpGet("paginate")]
        public IActionResult GetAll( 
                    [FromQuery] PaginationParams @params,
                    [FromQuery] String? denom,
                    [FromQuery] String? cat
                )
        {
            
            var count = _produitService.Count(denom, cat);
            var produits = _produitService.GetAll(cat, denom, @params.Page, @params.ItemsPerPage).ToList();
            var paginationMetadata =
                new PaginationMetadata<Produit>(count, @params.Page, @params.ItemsPerPage, produits);
            return Ok(paginationMetadata);
        }
        
        

        [HttpGet("{id}")]
        public IActionResult getOne(int id)
        {
            var produit = _produitService.GetById(id);
            return Ok(produit);
        }

        [HttpPost]
        public IActionResult Create(Produit produit)
        {
            var produitCreated = _produitService.Create(produit);
            return Ok(produitCreated);
        }

        /// <summary>
        /// Method permit to create a produit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Create(int id)
        {
            _produitService.Delete(id);
            return Ok(new { message = "Produit deleted", id = id });
        }
        /// <summary>
        /// Update a produit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Produit produit)
        {
            _produitService.Update(id, produit);
            return Ok(new { message = "Produit updated" });
        }


    }

}   