
using System.ComponentModel.DataAnnotations.Schema;

namespace Boardgames.Data.Models;

public class BoardgameSeller
{
    [ForeignKey(nameof(BoardgameId))]
    public int BoardgameId { get; set; }
    public  Boardgame ?Boardgame { get; set; }
    //•	BoardgameId – integer, Primary Key, foreign key(required)
    //•	Boardgame – Boardgame
    [ForeignKey(nameof(SellerId))]
    public int SellerId { get; set; }
    public Seller ?Seller { get; set; }
    //•	SellerId – integer, Primary Key, foreign key(required)
    //•	Seller – Seller

}


