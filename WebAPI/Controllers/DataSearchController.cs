﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.lib;

namespace WebAPI.Controllers {
    [RoutePrefix("api")]
    public class DataSearchController :ApiController {
        private WebClient client;
        private List<DataSearch> lista;

        [AcceptVerbs("GET")]
        [Route("get-consulta/{consulta}")]
        public IEnumerable<DataSearch> ConsultarGoogle(String consulta) {
            normalizarConsulta(ref consulta);

            client = new WebClient();
            lista = new List<DataSearch>();

            String responseHTML = client.DownloadString(Constantes.urlSearchGoogle + consulta);
            String substringHTML = "";
            String titulo;
            String url;
            Boolean existHTMLPattern = responseHTML.Length > 0;
            int posMatchStart = -1;
            int posMatchEnd = -1;

            while (existHTMLPattern) {
                titulo = "";
                url = "";
                existHTMLPattern = false;
                posMatchStart = responseHTML.IndexOf(Constantes.patternStart);

                if (posMatchStart > -1) {
                    posMatchStart += Constantes.lengthPatternStart;
                    responseHTML = responseHTML.Substring(posMatchStart, (responseHTML.Length - posMatchStart));
                    posMatchEnd = responseHTML.IndexOf(Constantes.patternEnd);
                    substringHTML = responseHTML.Substring(0, posMatchEnd + Constantes.lengthPatternEnd);

                    SetAtributos(ref titulo, ref url, substringHTML);

                    if ((!String.IsNullOrEmpty(titulo)) && (!String.IsNullOrEmpty(url))) {
                        lista.Add(new DataSearch() { Titulo = titulo, URL = url });
                    }

                    existHTMLPattern = true;
                }
            }

            return lista.Count > 0 ? lista : null;
        }

        private void normalizarConsulta(ref String pConsulta) {
            if (!String.IsNullOrEmpty(pConsulta)) {
                pConsulta = pConsulta.Replace(" ", "+");
            } else {
                throw new Exception("Texto de pesquisa não informado");
            }
        }

        private void SetAtributos(ref String pTitulo, ref String pURL, String pSubStringHTML) {
            if (pSubStringHTML.Contains("http")) {
                pSubStringHTML = WebUtility.HtmlDecode(pSubStringHTML);

                pTitulo = Regex.Replace(pSubStringHTML, "<.*?>", String.Empty);

                int posURL = pSubStringHTML.IndexOf(Constantes.patternStartURL) + Constantes.lengthPatternStartURL;
                pSubStringHTML = pSubStringHTML.Substring(posURL, (pSubStringHTML.Length - posURL));

                pURL = pSubStringHTML.Substring(0, pSubStringHTML.IndexOf(Constantes.patternEndURL));
                pURL = WebUtility.UrlDecode(pURL);
            }
        }
    }
}
