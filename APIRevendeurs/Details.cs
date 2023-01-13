using System;
namespace APIRevendeurs
{
	public class Details
	{
		public float Price { get; set; }
		public String? Description { get; set; }
		public String? Color { get; set; }

		public Details()
		{
		}

        public Details(float price, string description, string color)
        {
            Price = price;
            Description = description;
            Color = color;
        }
    }
}

