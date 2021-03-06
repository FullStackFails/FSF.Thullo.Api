﻿using System;

namespace FSF.Thullo.Core.Entities
{
  public class Card
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public int ListId { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
  }
}
