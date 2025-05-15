using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ShoppingCart
{
	private HttpContextBase context;

	public ShoppingCart(HttpContextBase httpContext)
	{
		context = httpContext;
	}

	private List<ShoppingCartDetails> CartLines
	{
		get { return (List<ShoppingCartDetails>)(context.Session[nameof(CartLines)] ?? (context.Session[nameof(CartLines)] = new List<ShoppingCartDetails>())); }
	}

	public decimal ShipFee
	{
		get { return (decimal)(context.Session[nameof(ShipFee)] ?? 0M); }
		set { context.Session[nameof(ShipFee)] = value; }
	}
	public string ShipProvince
	{
		get { return (string)(context.Session[nameof(ShipProvince)] ?? string.Empty); }
		set { context.Session[nameof(ShipProvince)] = value; }
	}

	public string ShipDistrict
	{
		get { return (string)(context.Session[nameof(ShipDistrict)] ?? string.Empty); }
		set { context.Session[nameof(ShipDistrict)] = value; }
	}

	public string ShipWard
	{
		get { return (string)(context.Session[nameof(ShipWard)] ?? string.Empty); }
		set { context.Session[nameof(ShipWard)] = value; }
	}

	public void Update(PP_Product product, string variation, int quantity)
	{
		if (product == null)
			return;

		var line = CartLines.Where(l => l.Product.Id == product.Id && l.Variation == variation).FirstOrDefault();
		var price = product.PromotionEnabled ? product.PromotionPrice : product.Price;

		if (line == null)
		{
			CartLines.Add(new ShoppingCartDetails()
			{
				Product = product,
				Variation = variation,
				Quantity = quantity,
				Price = price
			});
		}
		else if (quantity == 0)
		{
			CartLines.Remove(line);
			line = null;
		}
		else
		{
			line.Quantity = quantity;
			line.Price = price;
		}

		context.Session["MyCart"] = CartLines;
	}

	public void AddToCart(PP_Product product, string variation, int quantity)
	{
		if (product == null)
			return;

		var line = CartLines.Where(l => l.Product.Id == product.Id && l.Variation == variation).FirstOrDefault();
		var price = product.PromotionEnabled ? product.PromotionPrice : product.Price;

		if (line == null)
		{
			if (quantity > 1)
			{
				CartLines.Add(new ShoppingCartDetails()
				{

					Product = product,
					Variation = variation,
					Quantity = quantity,
					Price = price
				});
			}
			else
			{
				CartLines.Add(new ShoppingCartDetails()
				{

					Product = product,
					Variation = variation,
					Quantity = 1,
					Price = price
				});
			}

		}
		else
		{
			if (quantity > 1)
			{
				line.Quantity = line.Quantity + quantity;
				line.Price = price;
			}
			else
			{
				line.Quantity++;
				line.Price = price;
			}
		}
	}

	public void RemoveFromCart(PP_Product product, string variation)
	{
		var line = CartLines.Where(l => l.Product.Id == product.Id && l.Variation == variation).FirstOrDefault();

		if (line != null)
		{
			CartLines.Remove(line);
		}
	}

	public List<ShoppingCartDetails> GetCartLines()
	{
		return CartLines.ToList();
	}

	public List<OrderLine> GetOrderLines()
	{
		return CartLines.Select(i => new OrderLine()
		{
			id = i.Product.Id,
			Image = i.Product.ImageUrl,
			Brand = i.Product.Brand,
			Title = i.Product.Title,
			Variation = i.Variation,
			Price = i.Price,
			Quantity = i.Quantity,
			RowTotal = i.Price * i.Quantity,
			Weight = i.Product.Weight,
			PromotionEnabled = i.Product.PromotionEnabled
		}).ToList();
	}

	public int GetQuantity()
	{
		return CartLines.Count;
	}

	public int GetQuantity(PP_Product product)
	{
		var line = CartLines.Where(l => l.Product.Id == product.Id).FirstOrDefault();

		if (line == null)
			return 0;
		else return line.Quantity;
	}

	public decimal GetProductTotal(PP_Product product)
	{
		var line = CartLines.Where(l => l.Product.Id == product.Id).FirstOrDefault();

		if (line == null)
			return 0;
		else return line.Quantity * line.Price;
	}

	public decimal GetSubTotal()
	{
		decimal total = 0;
		foreach (var line in CartLines)
			total += line.Price * line.Quantity;

		return total;
	}

	public int GetTotalWeight()
	{
		int total = 0;
		foreach (var line in CartLines)
			total += line.Product.Weight;

		return total;
	}

	public decimal GetTotal()
	{
		return GetSubTotal() + ShipFee;
	}

	public void Clear()
	{
		CartLines.Clear();
	}
}