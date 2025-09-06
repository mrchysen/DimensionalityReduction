using ClippingAlgorithms.Models.Polygons;

namespace ClippingAlgorithms.Clippers;

public interface IClipper
{
	List<Polygon> Clip(List<Polygon> polygons);

	List<Polygon> Clip(Polygon polygon1, Polygon polygon2);
}
