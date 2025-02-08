using System;
using System.Collections.Generic;

namespace guitarshop.Models;

public partial class Review
{
    public int IdReviews { get; set; }

    public int UserReviewId { get; set; }

    public int GuitarId { get; set; }

    public int Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime ReviewDate { get; set; }

    public virtual CatalogProd Guitar { get; set; } = null!;

    public virtual User UserReview { get; set; } = null!;
}
