using System.ComponentModel.DataAnnotations;

namespace ApiUser.Entities;

public class Produit
{
    [Key]
    public int IdProduit { get; set; }
    public string DenominationProduit { get; set; }
    public string CategorieProduit { get; set; }
    public int QuantiteProduit { get; set; }
    public int PUProduit { get; set; }
}