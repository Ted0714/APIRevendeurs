using System;
namespace APIRevendeurs
{
	public class Produits
	{
		public DateTime Date { get; set; }
		public String? Name { get; set; }
		public Details? Details { get; set; }
		public int Stock { get; set; }
		public int Id { get; set; }

		public Produits()
		{
		}

        public Produits(DateTime date, string name, Details details, int stock, int id)
        {
            Date = date;
            Name = name;
            Details = details;
            Stock = stock;
            Id = id;
        }
    }
}