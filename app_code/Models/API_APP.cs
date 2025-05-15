using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using PetaPoco;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;

public class DateFormatConverterApp : IsoDateTimeConverter
{
	public DateFormatConverterApp(string format)
	{
		DateTimeFormat = format;
	}
}

public class REQ_Product_SUBMIT
{
	public List<Product> _Product { get; set; }
	public List<Category> _Category { get; set; }
	public Order _order { get; set; }

	public class Product
	{
		[Display(Name = "en:Language|vi:Ngôn ngữ")]
		public string LangId { get; set; }

		[Display(Name = "en:Page template|vi:Mẫu hiển thị")]
		public int PageId { get; set; }

		[Display(Name = "en:Mô tả sản phẩm|vi:Thông tin sản phẩm")]
		public string Des { get; set; }
		public string Title { get; set; }
		public int Id { get; set; }
		[Display(Name = "en:Type|vi:Phân loại")]
		public string NodeType { get; set; }

		[Display(Name = "en:Status|vi:Trạng thái")]
		public string NodeStatus { get; set; }

		[Display(Name = "en:Path|vi:Đường dẫn")]
		public string NodePath { get; set; }

		[Display(Name = "en:Product name|vi:Tên nhãn hiệu")]
		public string Brand { get; set; }

		[Display(Name = "en:Mô tả sản phẩm|vi:Mô tả sản phẩm")]
		public string Content { get; set; }

		[Display(Name = "en:Attributes enabled|vi: Bật/tắt thuộc tính phân loại")]
		public bool AttrbEnabled { get; set; }

		[Display(Name = "en:Attribute name|vi:Tên thuộc tính")]
		public string AttrbName { get; set; }

		[Display(Name = "en:Attribute values|vi:Giá trị thuộc tính")]
		public string AttrbValues { get; set; }

		[Display(Name = "en:Price|vi:Giá")]
		public decimal Price { get; set; }

		[Display(Name = "en:Promotion price|vi:Giá khuyến mãi")]
		public decimal PromotionPrice { get; set; }

		[Display(Name = "en:Promotion label|vi:Nhãn khuyến mãi")]
		public string PromotionLabel { get; set; }

		[Display(Name = "en:Promotion enabled|vi:Kích hoạt khuyến mãi")]
		public bool PromotionEnabled { get; set; }

		[Display(Name = "en:Promotion expiration|vi:Thời hạn khuyến mãi")]
		public DateTime? PromotionExpiration { get; set; }

		[Display(Name = "en:Best seller|vi:Sản phẩm bán chạy")]
		public bool BestSeller { get; set; }

		[Display(Name = "en:Product code|vi:Mã sản phẩm")]
		public string ProductCode { get; set; }

		[Display(Name = "en:Stock qty|vi:Số lượng tồn")]
		public int StockQty { get; set; }

		[Display(Name = "en:Product weight|vi:Trọng lượng hàng", Prompt = "Tính bằng gram")]
		public int Weight { get; set; }

		[Display(Name = "en:View counter|vi:Lượt xem")]
		public int ViewCounter { get; set; }

		[Display(Name = "en:Sold counter|vi:Lượt bán")]
		public int SoldCounter { get; set; }

		public string MetaDescription { get; set; }

		public string MetaKeywords { get; set; }

		public int? catid { get; set; }

		public int? IDcat { get; set; }
		public int? idnode { get; set; }

		public string listcat { get; set; }

		public string ImagesJson { get; set; }


		[NotMapped]
		[Ignore]
		public string Breadcrumb { get; set; }
	}

	public class Category
	{
		[Display(Name = "en:Languague|vi:Ngôn ngữ")]
		public string LangId { get; set; }
		public int Id { get; set; }
		[Display(Name = "en:Category|vi:Chuyên mục")]
		public int? ParentId { get; set; }

		[Display(Name = "en:List template|vi:Mẫu hiển thị danh sách")]
		public int PageId { get; set; }

		[Display(Name = "en:Item template|vi:Mẫu hiển thị phần tử")]
		public int PageIdItem { get; set; }

		[Display(Name = "en:Type|vi:Phân loại")]
		public string NodeType { get; set; }

		public int CategoryLevel { get; set; }

		[Display(Name = "en:Title|vi:Tiêu đề")]
		public string Title { get; set; }

		[Display(Name = "en:Breadcrumb|vi:Breadcrumb")]
		public string Breadcrumb { get; set; }

		[Display(Name = "en:Category path|vi:Đường dẫn")]
		public string CategoryPath { get; set; }

		[Display(Name = "en:Image|vi:Ảnh đại diện")]
		public string ImageUrl { get; set; }

		public string MetaDescription { get; set; }

		public string MetaKeywords { get; set; }

		public List<Product> _product { get; set; } = new List<Product>();

	}


	public class Order
	{
		public string id { get; set; }
		public string pick_name { get; set; }
		public string pick_address { get; set; }
		public string pick_province { get; set; }
		public string pick_district { get; set; }
		public string pick_ward { get; set; }
		public string pick_tel { get; set; }
		public string tel { get; set; }
		public string email { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public string province { get; set; }
		public string district { get; set; }
		public string ward { get; set; }
		public string stress { get; set; }
		public string hamlet { get; set; }
		public string note { get; set; }
		public int use_return_address { get; set; }
		public int is_freeship { get; set; }
		[JsonConverter(typeof(DateFormatConverterApp), "yyyy-MM-dd")]
		public DateTime pick_date { get; set; }
		public int pick_money { get; set; }
		public int value { get; set; }
		public string transport { get; set; }
		public string pick_option { get; set; }
		public string deliver_option { get; set; }
		public int pick_session { get; set; }
	}
}

public class RSP_PRODUCT_SUBMIT
{
	public bool success { get; set; }
	public string message { get; set; }
	public Order order { get; set; }
	public Error error { get; set; }
	public class Order
	{
		public string partner_id { get; set; }
		public string label { get; set; }
		public string area { get; set; }
		public int fee { get; set; }
		public string insurance_fee { get; set; }
		public string estimated_pick_time { get; set; }
		public string estimated_deliver_time { get; set; }
		public List<Product> products { get; set; }
		public int status_id { get; set; }

		public class Product
		{
			public string name { get; set; }
			public int price { get; set; }
			public double weight { get; set; }
			public int quantity { get; set; }
			public int product_code { get; set; }
		}
	}
	public class Error
	{
		public string code { get; set; }
		public string partner_id { get; set; }
		public string ghtk_label { get; set; }
		public DateTime created { get; set; }
		public int status { get; set; }
	}
}

public class REQ_PRODUCT_FEE
{
	public string pick_address { get; set; }
	public string pick_province { get; set; }
	public string pick_district { get; set; }
	public string pick_ward { get; set; }
	public string province { get; set; }
	public string district { get; set; }
	public string ward { get; set; }
	public string address { get; set; }
	public int weight { get; set; }
	public decimal value { get; set; }
	public string transport { get; set; }
	public string deliver_option { get; set; }
	public int tags { get; set; }
}

public class RSP_PRODUCT_FEE
{
	public bool success { get; set; }
	public string message { get; set; }
	public Fee fee { get; set; }

	public class ExtFee
	{
		public string display { get; set; }
		public string title { get; set; }
		public int amount { get; set; }
		public string type { get; set; }
	}

	public class Fee
	{
		public string name { get; set; }
		public decimal fee { get; set; }
		public int insurance_fee { get; set; }
		public string delivery_type { get; set; }
		public int a { get; set; }
		public string dt { get; set; }
		public ExtFee[] extFees { get; set; }
		public bool delivery { get; set; }
	}
}

public class RSP_PRODUCT_STATE
{
	public bool success { get; set; }
	public string message { get; set; }
	public Order order { get; set; }

	public class Order
	{
		public string label_id { get; set; }
		public string partner_id { get; set; }
		public int status { get; set; }
		public string status_text { get; set; }
		public string created { get; set; }
		public string modified { get; set; }
		public string message { get; set; }
		public string pick_date { get; set; }
		public string deliver_date { get; set; }
		public string customer_fullname { get; set; }
		public string customer_tel { get; set; }
		public string address { get; set; }
		public int storage_day { get; set; }
		public int ship_money { get; set; }
		public int insurance { get; set; }
		public int value { get; set; }
		public double weight { get; set; }
		public int pick_money { get; set; }
		public int is_freeship { get; set; }
	}
}

public class API_APP_CHECK
{
	private const string NETWORK_EXCEPTION = "NETWORK_EXCEPTION";
	private static string _baseAddr, _token;
	private static HttpClient _client = null;

	public static void Init(string baseAddr, string token)
	{
		_baseAddr = baseAddr;
		_token = token;
		_client = new HttpClient();
		_client.BaseAddress = new Uri(_baseAddr);
		_client.DefaultRequestHeaders.Add("Token", _token);
	}

	private static string ConvertToQueryString(object obj)
	{
		return string.Join("&", obj.GetType().GetProperties().Select(p => $"{p.Name}={Uri.EscapeDataString(Convert.ToString(p.GetValue(obj)))}"));
	}

	public static bool DownloadPdf(string label, Func<byte[], bool> success, Action<string> error = null)
	{
		HttpResponseMessage rsp = null;
		try
		{
			rsp = _client.GetAsync($"/services/label/{label}").Result;
		}
		catch
		{
			error?.Invoke(NETWORK_EXCEPTION);
			return false;
		}

		if (!rsp.IsSuccessStatusCode)
		{
			error?.Invoke(rsp.StatusCode.ToString());
			return false;
		}

		if (rsp.Content.Headers.ContentType.MediaType == "application/json")
		{
			error?.Invoke(rsp.Content.ReadAsStringAsync().Result);
			return false;
		}

		return success(rsp.Content.ReadAsByteArrayAsync().Result);
	}


	public static bool CheckOrder(string orderno, Func<RSP_PRODUCT_STATE, bool> success, Action<string> error)
	{
		HttpResponseMessage rsp = null;
		try
		{
			rsp = _client.GetAsync($"/services/shipment/v2/{orderno}").Result;
		}
		catch
		{
			error(NETWORK_EXCEPTION);
			return false;
		}

		if (!rsp.IsSuccessStatusCode)
		{
			error(rsp.StatusCode.ToString());
			return false;
		}

		var str = rsp.Content.ReadAsStringAsync().Result;
		return success(Json.Decode<RSP_PRODUCT_STATE>(str));
	}


	public static async Task<bool> SubmitProductAsync(REQ_Product_SUBMIT data, Func<RSP_PRODUCT_SUBMIT, bool> success, Action<string> error)
	{
		var json = JsonConvert.SerializeObject(data);
		var req = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
		HttpResponseMessage rsp = null;

		try
		{
			rsp = await _client.PostAsync("api/product/", req);
		}
		catch (Exception ex)
		{
			error(NETWORK_EXCEPTION);
			return false;
		}

		if (!rsp.IsSuccessStatusCode)
		{
			error(rsp.StatusCode.ToString());
			return false;
		}

		try
		{
			var str = await rsp.Content.ReadAsStringAsync(); // Đọc nội dung phản hồi bất đồng bộ
			var responseData = JsonConvert.DeserializeObject<RSP_PRODUCT_SUBMIT>(str); // Giải mã JSON phản hồi

			// Gọi hàm success với dữ liệu phản hồi và trả về kết quả
			return success(responseData);
		}
		catch (Exception ex)
		{
			error("Error in processing response: " + ex.Message); // Thông báo nếu có lỗi khi xử lý JSON
			return false;
		}
	}

	public static bool SubmitOrder(REQ_ORDER_SUBMIT data, Func<RSP_PRODUCT_SUBMIT, bool> success, Action<string> error)
	{
		var json = JsonConvert.SerializeObject(data);
		var req = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
		HttpResponseMessage rsp = null;
		try
		{
			rsp = _client.PostAsync("/services/shipment/order/?ver=1.5", req).Result;
		}
		catch
		{
			error(NETWORK_EXCEPTION);
			return false;
		}

		if (!rsp.IsSuccessStatusCode)
		{
			error(rsp.StatusCode.ToString());
			return false;
		}

		var str = rsp.Content.ReadAsStringAsync().Result;
		return success(Json.Decode<RSP_PRODUCT_SUBMIT>(str));
	}

	public static bool CaculateFee(REQ_PRODUCT_FEE data, Action<RSP_ORDER_FEE> success, Action<string> error)
	{
		var req = ConvertToQueryString(data);

		var client = new HttpClient();
		client.BaseAddress = new Uri(_baseAddr);
		client.DefaultRequestHeaders.Add("Token", _token);

		HttpResponseMessage rsp = null;
		try
		{
			rsp = client.GetAsync($"/services/shipment/fee?{req}&tags%5B%5D=0").Result;
		}
		catch
		{
			error(NETWORK_EXCEPTION);
			return false;
		}

		if (!rsp.IsSuccessStatusCode)
		{
			error(rsp.StatusCode.ToString());
			return false;
		}

		var str = rsp.Content.ReadAsStringAsync().Result;
		success(Json.Decode<RSP_ORDER_FEE>(str));
		return true;
	}

	public static string GetGhtkStatus(int code)
	{
		switch (code)
		{
			case -1: return "Hủy đơn hàng";
			case 1: return "Chưa tiếp nhận";
			case 2: return "Đã tiếp nhận";
			case 3: return "Đã lấy hàng/Đã nhập kho";
			case 4: return "Đã điều phối giao hàng/Đang giao hàng";
			case 5: return "Đã giao hàng/Chưa đối soát";
			case 6: return "Đã đối soát";
			case 7: return "Không lấy được hàng";
			case 8: return "Hoãn lấy hàng";
			case 9: return "Không giao được hàng";
			case 10: return "Delay giao hàng";
			case 11: return "Đã đối soát công nợ trả hàng";
			case 12: return "Đã điều phối lấy hàng/Đang lấy hàng";
			case 13: return "Đơn hàng bồi hoàn";
			case 20: return "Đang trả hàng (COD cầm hàng đi trả)";
			case 21: return "Đã trả hàng (COD đã trả xong hàng)";
			case 123: return "Shipper báo đã lấy hàng";
			case 127: return "Shipper (nhân viên lấy/giao hàng) báo không lấy được hàng";
			case 128: return "Shipper báo delay lấy hàng";
			case 45: return "Shipper báo đã giao hàng";
			case 49: return "Shipper báo không giao được giao hàng";
			case 410: return "Shipper báo delay giao hàng";
		}

		return "NA";
	}
}