#region Copyright (C) 2003-2013 Stimulsoft
/*
{*******************************************************************}
{																	}
{	Stimulsoft Reports.RT											}
{	                         										}
{																	}
{	Copyright (C) 2003-2013 Stimulsoft     							}
{	ALL RIGHTS RESERVED												}
{																	}
{	The entire contents of this file is protected by U.S. and		}
{	International Copyright Laws. Unauthorized reproduction,		}
{	reverse-engineering, and distribution of all or any portion of	}
{	the code contained in this file is strictly prohibited and may	}
{	result in severe civil and criminal penalties and will be		}
{	prosecuted to the maximum extent possible under the law.		}
{																	}
{	RESTRICTIONS													}
{																	}
{	THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES			}
{	ARE CONFIDENTIAL AND PROPRIETARY								}
{	TRADE SECRETS OF Stimulsoft										}
{																	}
{	CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON		}
{	ADDITIONAL RESTRICTIONS.										}
{																	}
{*******************************************************************}
*/
#endregion Copyright (C) 2003-2013 Stimulsoft

using System;
using System.Collections.Generic;

public class Data
{
    #region classes
    private List<CustomersItem> customersItems = new List<CustomersItem>();
    public List<CustomersItem> Customers
    {
        get
        {
            return customersItems;
        }
        set
        {
            customersItems = value;
        }
    }

    public class CustomersItem
    {
        internal System.String customerID;
        public System.String CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
            }
        }

        private System.String companyName;
        public System.String CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }

        private System.String contactName;
        public System.String ContactName
        {
            get
            {
                return contactName;
            }
            set
            {
                contactName = value;
            }
        }

        private System.String contactTitle;
        public System.String ContactTitle
        {
            get
            {
                return contactTitle;
            }
            set
            {
                contactTitle = value;
            }
        }

        private System.String address;
        public System.String Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        private System.String city;
        public System.String City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        private System.String region;
        public System.String Region
        {
            get
            {
                return region;
            }
            set
            {
                region = value;
            }
        }

        private System.String postalCode;
        public System.String PostalCode
        {
            get
            {
                return postalCode;
            }
            set
            {
                postalCode = value;
            }
        }

        private System.String country;
        public System.String Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

        private System.String phone;
        public System.String Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        private System.String fax;
        public System.String Fax
        {
            get
            {
                return fax;
            }
            set
            {
                fax = value;
            }
        }

        public CustomersItem(System.String customerID, System.String companyName, System.String contactName, System.String contactTitle, System.String address, System.String city, System.String region, System.String postalCode, System.String country, System.String phone, System.String fax)
        {
            this.customerID = customerID;
            this.companyName = companyName;
            this.contactName = contactName;
            this.contactTitle = contactTitle;
            this.address = address;
            this.city = city;
            this.region = region;
            this.postalCode = postalCode;
            this.country = country;
            this.phone = phone;
            this.fax = fax;
        }
    }
    #endregion

    public Data()
    {
        #region Init
        this.customersItems.Add(new CustomersItem("ALFKI", "Alfreds Futterkiste", "Maria Anders", "Sales Representative", "Obere Str. 57", "Berlin", null, "12209", "Germany", "030-0074321", "030-0076545"));
        this.customersItems.Add(new CustomersItem("ANATR", "Ana Trujillo Emparedados y helados", "Ana Trujillo", "Owner", "Avda. de la Constitución 2222", "México D.F.", null, "05021", "Mexico", "(5) 555-4729", "(5) 555-3745"));
        this.customersItems.Add(new CustomersItem("ANTON", "Antonio Moreno Taquería", "Antonio Moreno", "Owner", "Mataderos  2312", "México D.F.", null, "05023", "Mexico", "(5) 555-3932", null));
        this.customersItems.Add(new CustomersItem("AROUT", "Around the Horn", "Thomas Hardy", "Sales Representative", "120 Hanover Sq.", "London", null, "WA1 1DP", "UK", "(171) 555-7788", "(171) 555-6750"));
        this.customersItems.Add(new CustomersItem("BERGS", "Berglunds snabbköp", "Christina Berglund", "Order Administrator", "Berguvsvägen  8", "Luleå", null, "S-958 22", "Sweden", "0921-12 34 65", "0921-12 34 67"));
        this.customersItems.Add(new CustomersItem("BLAUS", "Blauer See Delikatessen", "Hanna Moos", "Sales Representative", "Forsterstr. 57", "Mannheim", null, "68306", "Germany", "0621-08460", "0621-08924"));
        this.customersItems.Add(new CustomersItem("BLONP", "Blondesddsl père et fils", "Frédérique Citeaux", "Marketing Manager", "24, place Kléber", "Strasbourg", null, "67000", "France", "88.60.15.31", "88.60.15.32"));
        this.customersItems.Add(new CustomersItem("BOLID", "Bólido Comidas preparadas", "Martín Sommer", "Owner", "C/ Araquil, 67", "Madrid", null, "28023", "Spain", "(91) 555 22 82", "(91) 555 91 99"));
        this.customersItems.Add(new CustomersItem("BONAP", "Bon app'", "Laurence Lebihan", "Owner", "12, rue des Bouchers", "Marseille", null, "13008", "France", "91.24.45.40", "91.24.45.41"));
        this.customersItems.Add(new CustomersItem("BOTTM", "Bottom-Dollar Markets", "Elizabeth Lincoln", "Accounting Manager", "23 Tsawassen Blvd.", "Tsawassen", "BC", "T2F 8M4", "Canada", "(604) 555-4729", "(604) 555-3745"));
        this.customersItems.Add(new CustomersItem("BSBEV", "B's Beverages", "Victoria Ashworth", "Sales Representative", "Fauntleroy Circus", "London", null, "EC2 5NT", "UK", "(171) 555-1212", null));
        this.customersItems.Add(new CustomersItem("CACTU", "Cactus Comidas para llevar", "Patricio Simpson", "Sales Agent", "Cerrito 333", "Buenos Aires", null, "1010", "Argentina", "(1) 135-5555", "(1) 135-4892"));
        this.customersItems.Add(new CustomersItem("CENTC", "Centro comercial Moctezuma", "Francisco Chang", "Marketing Manager", "Sierras de Granada 9993", "México D.F.", null, "05022", "Mexico", "(5) 555-3392", "(5) 555-7293"));
        this.customersItems.Add(new CustomersItem("CHOPS", "Chop-suey Chinese", "Yang Wang", "Owner", "Hauptstr. 29", "Bern", null, "3012", "Switzerland", "0452-076545", null));
        this.customersItems.Add(new CustomersItem("COMMI", "Comércio Mineiro", "Pedro Afonso", "Sales Associate", "Av. dos Lusíadas, 23", "Sao Paulo", "SP", "05432-043", "Brazil", "(11) 555-7647", null));
        this.customersItems.Add(new CustomersItem("CONSH", "Consolidated Holdings", "Elizabeth Brown", "Sales Representative", "Berkeley Gardens 12  Brewery", "London", null, "WX1 6LT", "UK", "(171) 555-2282", "(171) 555-9199"));
        this.customersItems.Add(new CustomersItem("WANDK", "Die Wandernde Kuh", "Rita Müller", "Sales Representative", "Adenauerallee 900", "Stuttgart", null, "70563", "Germany", "0711-020361", "0711-035428"));
        this.customersItems.Add(new CustomersItem("DRACD", "Drachenblut Delikatessen", "Sven Ottlieb", "Order Administrator", "Walserweg 21", "Aachen", null, "52066", "Germany", "0241-039123", "0241-059428"));
        this.customersItems.Add(new CustomersItem("DUMON", "Du monde entier", "Janine Labrune", "Owner", "67, rue des Cinquante Otages", "Nantes", null, "44000", "France", "40.67.88.88", "40.67.89.89"));
        this.customersItems.Add(new CustomersItem("EASTC", "Eastern Connection", "Ann Devon", "Sales Agent", "35 King George", "London", null, "WX3 6FW", "UK", "(171) 555-0297", "(171) 555-3373"));
        this.customersItems.Add(new CustomersItem("ERNSH", "Ernst Handel", "Roland Mendel", "Sales Manager", "Kirchgasse 6", "Graz", null, "8010", "Austria", "7675-3425", "7675-3426"));
        this.customersItems.Add(new CustomersItem("FAMIA", "Familia Arquibaldo", "Aria Cruz", "Marketing Assistant", "Rua Orós, 92", "Sao Paulo", "SP", "05442-030", "Brazil", "(11) 555-9857", null));
        this.customersItems.Add(new CustomersItem("FISSA", "FISSA Fabrica Inter. Salchichas S.A.", "Diego Roel", "Accounting Manager", "C/ Moralzarzal, 86", "Madrid", null, "28034", "Spain", "(91) 555 94 44", "(91) 555 55 93"));
        this.customersItems.Add(new CustomersItem("FOLIG", "Folies gourmandes", "Martine Rancé", "Assistant Sales Agent", "184, chaussée de Tournai", "Lille", null, "59000", "France", "20.16.10.16", "20.16.10.17"));
        this.customersItems.Add(new CustomersItem("FOLKO", "Folk och fä HB", "Maria Larsson", "Owner", "Åkergatan 24", "Bräcke", null, "S-844 67", "Sweden", "0695-34 67 21", null));
        this.customersItems.Add(new CustomersItem("FRANK", "Frankenversand", "Peter Franken", "Marketing Manager", "Berliner Platz 43", "München", null, "80805", "Germany", "089-0877310", "089-0877451"));
        this.customersItems.Add(new CustomersItem("FRANR", "France restauration", "Carine Schmitt", "Marketing Manager", "54, rue Royale", "Nantes", null, "44000", "France", "40.32.21.21", "40.32.21.20"));
        this.customersItems.Add(new CustomersItem("FRANS", "Franchi S.p.A.", "Paolo Accorti", "Sales Representative", "Via Monte Bianco 34", "Torino", null, "10100", "Italy", "011-4988260", "011-4988261"));
        this.customersItems.Add(new CustomersItem("FURIB", "Furia Bacalhau e Frutos do Mar", "Lino Rodriguez", "Sales Manager", "Jardim das rosas n. 32", "Lisboa", null, "1675", "Portugal", "(1) 354-2534", "(1) 354-2535"));
        this.customersItems.Add(new CustomersItem("GALED", "Galería del gastrónomo", "Eduardo Saavedra", "Marketing Manager", "Rambla de Cataluña, 23", "Barcelona", null, "08022", "Spain", "(93) 203 4560", "(93) 203 4561"));
        this.customersItems.Add(new CustomersItem("GODOS", "Godos Cocina Típica", "José Pedro Freyre", "Sales Manager", "C/ Romero, 33", "Sevilla", null, "41101", "Spain", "(95) 555 82 82", null));
        this.customersItems.Add(new CustomersItem("GOURL", "Gourmet Lanchonetes", "André Fonseca", "Sales Associate", "Av. Brasil, 442", "Campinas", "SP", "04876-786", "Brazil", "(11) 555-9482", null));
        this.customersItems.Add(new CustomersItem("GREAL", "Great Lakes Food Market", "Howard Snyder", "Marketing Manager", "2732 Baker Blvd.", "Eugene", "OR", "97403", "USA", "(503) 555-7555", null));
        this.customersItems.Add(new CustomersItem("GROSR", "GROSELLA-Restaurante", "Manuel Pereira", "Owner", "5ª Ave. Los Palos Grandes", "Caracas", "DF", "1081", "Venezuela", "(2) 283-2951", "(2) 283-3397"));
        this.customersItems.Add(new CustomersItem("HANAR", "Hanari Carnes", "Mario Pontes", "Accounting Manager", "Rua do Paço, 67", "Rio de Janeiro", "RJ", "05454-876", "Brazil", "(21) 555-0091", "(21) 555-8765"));
        this.customersItems.Add(new CustomersItem("HILAA", "HILARION-Abastos", "Carlos Hernández", "Sales Representative", "Carrera 22 con Ave. Carlos Soublette #8-35", "San Cristóbal", "Táchira", "5022", "Venezuela", "(5) 555-1340", "(5) 555-1948"));
        this.customersItems.Add(new CustomersItem("HUNGC", "Hungry Coyote Import Store", "Yoshi Latimer", "Sales Representative", "City Center Plaza 516 Main St.", "Elgin", "OR", "97827", "USA", "(503) 555-6874", "(503) 555-2376"));
        this.customersItems.Add(new CustomersItem("HUNGO", "Hungry Owl All-Night Grocers", "Patricia McKenna", "Sales Associate", "8 Johnstown Road", "Cork", "Co. Cork", null, "Ireland", "2967 542", "2967 3333"));
        this.customersItems.Add(new CustomersItem("ISLAT", "Island Trading", "Helen Bennett", "Marketing Manager", "Garden House Crowther Way", "Cowes", "Isle of Wight", "PO31 7PJ", "UK", "(198) 555-8888", null));
        this.customersItems.Add(new CustomersItem("KOENE", "Königlich Essen", "Philip Cramer", "Sales Associate", "Maubelstr. 90", "Brandenburg", null, "14776", "Germany", "0555-09876", null));
        this.customersItems.Add(new CustomersItem("LACOR", "La corne d'abondance", "Daniel Tonini", "Sales Representative", "67, avenue de l'Europe", "Versailles", null, "78000", "France", "30.59.84.10", "30.59.85.11"));
        this.customersItems.Add(new CustomersItem("LAMAI", "La maison d'Asie", "Annette Roulet", "Sales Manager", "1 rue Alsace-Lorraine", "Toulouse", null, "31000", "France", "61.77.61.10", "61.77.61.11"));
        this.customersItems.Add(new CustomersItem("LAUGB", "Laughing Bacchus Wine Cellars", "Yoshi Tannamuri", "Marketing Assistant", "1900 Oak St.", "Vancouver", "BC", "V3F 2K1", "Canada", "(604) 555-3392", "(604) 555-7293"));
        this.customersItems.Add(new CustomersItem("LAZYK", "Lazy K Kountry Store", "John Steel", "Marketing Manager", "12 Orchestra Terrace", "Walla Walla", "WA", "99362", "USA", "(509) 555-7969", "(509) 555-6221"));
        this.customersItems.Add(new CustomersItem("LEHMS", "Lehmanns Marktstand", "Renate Messner", "Sales Representative", "Magazinweg 7", "Frankfurt a.M.", null, "60528", "Germany", "069-0245984", "069-0245874"));
        this.customersItems.Add(new CustomersItem("LETSS", "Let's Stop N Shop", "Jaime Yorres", "Owner", "87 Polk St. Suite 5", "San Francisco", "CA", "94117", "USA", "(415) 555-5938", null));
        this.customersItems.Add(new CustomersItem("LILAS", "LILA-Supermercado", "Carlos González", "Accounting Manager", "Carrera 52 con Ave. Bolívar #65-98 Llano Largo", "Barquisimeto", "Lara", "3508", "Venezuela", "(9) 331-6954", "(9) 331-7256"));
        this.customersItems.Add(new CustomersItem("LINOD", "LINO-Delicateses", "Felipe Izquierdo", "Owner", "Ave. 5 de Mayo Porlamar", "I. de Margarita", "Nueva Esparta", "4980", "Venezuela", "(8) 34-56-12", "(8) 34-93-93"));
        this.customersItems.Add(new CustomersItem("LONEP", "Lonesome Pine Restaurant", "Fran Wilson", "Sales Manager", "89 Chiaroscuro Rd.", "Portland", "OR", "97219", "USA", "(503) 555-9573", "(503) 555-9646"));
        this.customersItems.Add(new CustomersItem("MAGAA", "Magazzini Alimentari Riuniti", "Giovanni Rovelli", "Marketing Manager", "Via Ludovico il Moro 22", "Bergamo", null, "24100", "Italy", "035-640230", "035-640231"));
        this.customersItems.Add(new CustomersItem("MAISD", "Maison Dewey", "Catherine Dewey", "Sales Agent", "Rue Joseph-Bens 532", "Bruxelles", null, "B-1180", "Belgium", "(02) 201 24 67", "(02) 201 24 68"));
        this.customersItems.Add(new CustomersItem("MEREP", "Mère Paillarde", "Jean Fresnière", "Marketing Assistant", "43 rue St. Laurent", "Montréal", "Québec", "H1J 1C3", "Canada", "(514) 555-8054", "(514) 555-8055"));
        this.customersItems.Add(new CustomersItem("MORGK", "Morgenstern Gesundkost", "Alexander Feuer", "Marketing Assistant", "Heerstr. 22", "Leipzig", null, "04179", "Germany", "0342-023176", null));
        this.customersItems.Add(new CustomersItem("NORTS", "North/South", "Simon Crowther", "Sales Associate", "South House 300 Queensbridge", "London", null, "SW7 1RZ", "UK", "(171) 555-7733", "(171) 555-2530"));
        this.customersItems.Add(new CustomersItem("OCEAN", "Océano Atlántico Ltda.", "Yvonne Moncada", "Sales Agent", "Ing. Gustavo Moncada 8585 Piso 20-A", "Buenos Aires", null, "1010", "Argentina", "(1) 135-5333", "(1) 135-5535"));
        this.customersItems.Add(new CustomersItem("OLDWO", "Old World Delicatessen", "Rene Phillips", "Sales Representative", "2743 Bering St.", "Anchorage", "AK", "99508", "USA", "(907) 555-7584", "(907) 555-2880"));
        this.customersItems.Add(new CustomersItem("OTTIK", "Ottilies Käseladen", "Henriette Pfalzheim", "Owner", "Mehrheimerstr. 369", "Köln", null, "50739", "Germany", "0221-0644327", "0221-0765721"));
        this.customersItems.Add(new CustomersItem("PARIS", "Paris spécialités", "Marie Bertrand", "Owner", "265, boulevard Charonne", "Paris", null, "75012", "France", "(1) 42.34.22.66", "(1) 42.34.22.77"));
        this.customersItems.Add(new CustomersItem("PERIC", "Pericles Comidas clásicas", "Guillermo Fernández", "Sales Representative", "Calle Dr. Jorge Cash 321", "México D.F.", null, "05033", "Mexico", "(5) 552-3745", "(5) 545-3745"));
        this.customersItems.Add(new CustomersItem("PICCO", "Piccolo und mehr", "Georg Pipps", "Sales Manager", "Geislweg 14", "Salzburg", null, "5020", "Austria", "6562-9722", "6562-9723"));
        this.customersItems.Add(new CustomersItem("PRINI", "Princesa Isabel Vinhos", "Isabel de Castro", "Sales Representative", "Estrada da saúde n. 58", "Lisboa", null, "1756", "Portugal", "(1) 356-5634", null));
        this.customersItems.Add(new CustomersItem("QUEDE", "Que Delícia", "Bernardo Batista", "Accounting Manager", "Rua da Panificadora, 12", "Rio de Janeiro", "RJ", "02389-673", "Brazil", "(21) 555-4252", "(21) 555-4545"));
        this.customersItems.Add(new CustomersItem("QUEEN", "Queen Cozinha", "Lúcia Carvalho", "Marketing Assistant", "Alameda dos Canàrios, 891", "Sao Paulo", "SP", "05487-020", "Brazil", "(11) 555-1189", null));
        this.customersItems.Add(new CustomersItem("QUICK", "QUICK-Stop", "Horst Kloss", "Accounting Manager", "Taucherstraße 10", "Cunewalde", null, "01307", "Germany", "0372-035188", null));
        this.customersItems.Add(new CustomersItem("RANCH", "Rancho grande", "Sergio Gutiérrez", "Sales Representative", "Av. del Libertador 900", "Buenos Aires", null, "1010", "Argentina", "(1) 123-5555", "(1) 123-5556"));
        this.customersItems.Add(new CustomersItem("RATTC", "Rattlesnake Canyon Grocery", "Paula Wilson", "Assistant Sales Representative", "2817 Milton Dr.", "Albuquerque", "NM", "87110", "USA", "(505) 555-5939", "(505) 555-3620"));
        this.customersItems.Add(new CustomersItem("REGGC", "Reggiani Caseifici", "Maurizio Moroni", "Sales Associate", "Strada Provinciale 124", "Reggio Emilia", null, "42100", "Italy", "0522-556721", "0522-556722"));
        this.customersItems.Add(new CustomersItem("RICAR", "Ricardo Adocicados", "Janete Limeira", "Assistant Sales Agent", "Av. Copacabana, 267", "Rio de Janeiro", "RJ", "02389-890", "Brazil", "(21) 555-3412", null));
        this.customersItems.Add(new CustomersItem("RICSU", "Richter Supermarkt", "Michael Holz", "Sales Manager", "Grenzacherweg 237", "Genève", null, "1203", "Switzerland", "0897-034214", null));
        this.customersItems.Add(new CustomersItem("ROMEY", "Romero y tomillo", "Alejandra Camino", "Accounting Manager", "Gran Vía, 1", "Madrid", null, "28001", "Spain", "(91) 745 6200", "(91) 745 6210"));
        this.customersItems.Add(new CustomersItem("SANTG", "Santé Gourmet", "Jonas Bergulfsen", "Owner", "Erling Skakkes gate 78", "Stavern", null, "4110", "Norway", "07-98 92 35", "07-98 92 47"));
        this.customersItems.Add(new CustomersItem("SAVEA", "Save-a-lot Markets", "Jose Pavarotti", "Sales Representative", "187 Suffolk Ln.", "Boise", "ID", "83720", "USA", "(208) 555-8097", null));
        this.customersItems.Add(new CustomersItem("SEVES", "Seven Seas Imports", "Hari Kumar", "Sales Manager", "90 Wadhurst Rd.", "London", null, "OX15 4NB", "UK", "(171) 555-1717", "(171) 555-5646"));
        this.customersItems.Add(new CustomersItem("SIMOB", "Simons bistro", "Jytte Petersen", "Owner", "Vinbæltet 34", "Kobenhavn", null, "1734", "Denmark", "31 12 34 56", "31 13 35 57"));
        this.customersItems.Add(new CustomersItem("SPECD", "Spécialités du monde", "Dominique Perrier", "Marketing Manager", "25, rue Lauriston", "Paris", null, "75016", "France", "(1) 47.55.60.10", "(1) 47.55.60.20"));
        this.customersItems.Add(new CustomersItem("SPLIR", "Split Rail Beer & Ale", "Art Braunschweiger", "Sales Manager", "P.O. Box 555", "Lander", "WY", "82520", "USA", "(307) 555-4680", "(307) 555-6525"));
        this.customersItems.Add(new CustomersItem("SUPRD", "Suprêmes délices", "Pascale Cartrain", "Accounting Manager", "Boulevard Tirou, 255", "Charleroi", null, "B-6000", "Belgium", "(071) 23 67 22 20", "(071) 23 67 22 21"));
        this.customersItems.Add(new CustomersItem("THEBI", "The Big Cheese", "Liz Nixon", "Marketing Manager", "89 Jefferson Way Suite 2", "Portland", "OR", "97201", "USA", "(503) 555-3612", null));
        this.customersItems.Add(new CustomersItem("THECR", "The Cracker Box", "Liu Wong", "Marketing Assistant", "55 Grizzly Peak Rd.", "Butte", "MT", "59801", "USA", "(406) 555-5834", "(406) 555-8083"));
        this.customersItems.Add(new CustomersItem("TOMSP", "Toms Spezialitäten", "Karin Josephs", "Marketing Manager", "Luisenstr. 48", "Münster", null, "44087", "Germany", "0251-031259", "0251-035695"));
        this.customersItems.Add(new CustomersItem("TORTU", "Tortuga Restaurante", "Miguel Angel Paolino", "Owner", "Avda. Azteca 123", "México D.F.", null, "05033", "Mexico", "(5) 555-2933", null));
        this.customersItems.Add(new CustomersItem("TRADH", "Tradição Hipermercados", "Anabela Domingues", "Sales Representative", "Av. Inês de Castro, 414", "Sao Paulo", "SP", "05634-030", "Brazil", "(11) 555-2167", "(11) 555-2168"));
        this.customersItems.Add(new CustomersItem("TRAIH", "Trail's Head Gourmet Provisioners", "Helvetius Nagy", "Sales Associate", "722 DaVinci Blvd.", "Kirkland", "WA", "98034", "USA", "(206) 555-8257", "(206) 555-2174"));
        this.customersItems.Add(new CustomersItem("VAFFE", "Vaffeljernet", "Palle Ibsen", "Sales Manager", "Smagsloget 45", "Århus", null, "8200", "Denmark", "86 21 32 43", "86 22 33 44"));
        this.customersItems.Add(new CustomersItem("VICTE", "Victuailles en stock", "Mary Saveley", "Sales Agent", "2, rue du Commerce", "Lyon", null, "69004", "France", "78.32.54.86", "78.32.54.87"));
        this.customersItems.Add(new CustomersItem("VINET", "Vins et alcools Chevalier", "Paul Henriot", "Accounting Manager", "59 rue de l'Abbaye", "Reims", null, "51100", "France", "26.47.15.10", "26.47.15.11"));        
        this.customersItems.Add(new CustomersItem("WARTH", "Wartian Herkku", "Pirkko Koskitalo", "Accounting Manager", "Torikatu 38", "Oulu", null, "90110", "Finland", "981-443655", "981-443655"));
        this.customersItems.Add(new CustomersItem("WELLI", "Wellington Importadora", "Paula Parente", "Sales Manager", "Rua do Mercado, 12", "Resende", "SP", "08737-363", "Brazil", "(14) 555-8122", null));
        this.customersItems.Add(new CustomersItem("WHITC", "White Clover Markets", "Karl Jablonski", "Owner", "305 - 14th Ave. S. Suite 3B", "Seattle", "WA", "98128", "USA", "(206) 555-4112", "(206) 555-4115"));
        this.customersItems.Add(new CustomersItem("WILMK", "Wilman Kala", "Matti Karttunen", "Owner/Marketing Assistant", "Keskuskatu 45", "Helsinki", null, "21240", "Finland", "90-224 8858", "90-224 8858"));
        this.customersItems.Add(new CustomersItem("WOLZA", "Wolski  Zajazd", "Zbyszek Piestrzeniewicz", "Owner", "ul. Filtrowa 68", "Warszawa", null, "01-012", "Poland", "(26) 642-7012", "(26) 642-7012"));
        #endregion
    }
}