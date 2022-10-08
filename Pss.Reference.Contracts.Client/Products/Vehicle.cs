namespace Pss.Reference.Contracts.Client.Products;

public class Vehicle : ProductBase
{
	public string VehicleIdentificationNumber { get; set; }
	public string Make { get; set; }
	public string Model { get; set; }
	public short ManufactureYear { get; set; }
	public string LicenseNumber { get; set; }
}
