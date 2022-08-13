using Microsoft.EntityFrameworkCore;

namespace ApiUser.Services;

using ApiUser.Entities;
using ApiUser.Helpers;

public interface IProduitService
{
    IEnumerable<Produit> GetAll( String? denomination, String? cat, int page = 1, int take = 10);
    Produit GetById(int id);
    Produit Create(Produit produit);
    Produit Update(int id, Produit produit);
    void Delete(int id);
    int Count(String? denom, String? cat);
}

public class ProduitService : IProduitService
{
    private DatabaseContext _context;
    private int take = 10;
    private int page = 1;

    public ProduitService(DatabaseContext context)
    {
        this._context = context;
    }

    public Produit Create(Produit produit)
    {
        if (this._context.Produits.Any(p => p.DenominationProduit == p.CategorieProduit))
        {
            throw new Exception("User already exists");
        }
        this._context.Produits.Add(produit);
        this._context.SaveChanges();
        return produit;
    }

    public void Delete(int id)
    {
        var produit = this._context.Produits.Find(id);

        if (produit == null) throw new KeyNotFoundException("User not found in database");

        _context.Produits.Remove(produit);
        _context.SaveChanges();
    }

    public IEnumerable<Produit> GetAll( String? denom, String? cat,  int page = 1, int take = 10)
    { 

        page = page < 1 ? this.page : page;
        take = take < 1 ? this.take : take;
        //String q = String.Empty() 

        List<Produit> produits = new List<Produit>();
        
         
        if (cat != null || cat != null)
        {
            
            if (cat != null && denom != null)
            {
                produits  = this._context
                    .Produits
                    .Where(p => p.DenominationProduit.ToLower().StartsWith(denom.ToLower()) || p.CategorieProduit.ToLower().StartsWith(cat.ToLower()))
                    .Skip((page -1) * take)
                    .Take(take)
                    .ToList();
            }  else if (denom != null)
            {
             
                produits =  this._context
                    .Produits
                    .Where(p => p.DenominationProduit.ToLower().StartsWith(denom.ToLower()))
                    .Skip((page -1) * take)
                    .Take(take)
                    .ToList();
            }
            else if  (cat != null)
            {
                produits =  this._context
                    .Produits
                    .Where(p => p.CategorieProduit.ToLower().StartsWith(cat.ToLower()))
                    .Skip((page -1) * take)
                    .Take(take)
                    .ToList();
                
            } 
        }
        else {
            produits =  this._context
                .Produits
                .Where(p => true)
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();
        }

        return produits;


    }

    public Produit GetById(int id)
    {
        var produit = this._context.Produits.Find(id);

        if (produit == null) throw new KeyNotFoundException("User not found in database");

        return produit;
    }

    public Produit Update(int id, Produit produit)
    {
        var produitFound = this.getOneById(id);

        if (produitFound == null) throw new KeyNotFoundException("Produit not found in database");

        produitFound.DenominationProduit = produit.DenominationProduit;
        produitFound.CategorieProduit = produit.CategorieProduit;
        produitFound.QuantiteProduit = produit.QuantiteProduit;
        produitFound.PUProduit = produit.PUProduit;

        _context.Produits.Update(produitFound);
        _context.SaveChanges();

        return produitFound;
    }

    public Produit? getOneById(int id)
    {
        var produit = this._context.Produits.Find(id);
        return produit;
    }

    public int Count(String? denom, String? cat)
    {
        var count = 0;
        
        
        
        if (cat != null || cat != null)
        {
            
            if (cat != null && denom != null)
            {
                count = this._context
                    .Produits
                    .Where(p => p.DenominationProduit.ToLower().StartsWith(denom.ToLower()) || p.CategorieProduit.ToLower().StartsWith(cat.ToLower()))
                    .Count(u => true);
            }  else if (denom != null)
            {
             
                count = this._context
                    .Produits
                    .Where(p => p.DenominationProduit.ToLower().StartsWith(denom.ToLower()))
                    .Count(u => true);
            }
            else if  (cat != null)
            {
                count = this._context
                    .Produits
                    .Where(p => p.CategorieProduit.ToLower().StartsWith(cat.ToLower()))
                    .Count(u => true);
                
            } 
        } else {
            count = this._context.Users.Count(u => true);
        }
        return count;
        }
}