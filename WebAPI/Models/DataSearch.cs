using System;

namespace WebAPI.Models {
    public class DataSearch {
        private String titulo;
        private String url;

        public DataSearch() {}

        public DataSearch(string titulo, string uRL) {
            this.Titulo = titulo;
            this.URL = uRL;
        }

        public String Titulo {
            get {
                return titulo;
            }

            set {
                titulo = value;
            }
        }

        public String URL {
            get {
                return url;
            }

            set {
                url = value;
            }
        }
    }
}
