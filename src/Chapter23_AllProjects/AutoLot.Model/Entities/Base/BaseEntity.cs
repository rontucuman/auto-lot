namespace AutoLot.Model.Entities.Base
{
  public abstract class BaseEntity
  {
    public int Id { get; set; }
    public byte[]? TimeStamp { get; set; }
  }
}
