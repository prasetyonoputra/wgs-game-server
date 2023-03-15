using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;
using UnityTest.Network;
using Wargaming.Core.GlobalParam;
using Wargaming.Core.GlobalParam.HelperScenarioAktif;
namespace Wargaming.Core.Network
{
    public class WargamingAPI : MonoBehaviour
    {
        static bool enableDebug = false;

        public static async Task<string> GetDetailSatuan(string ID, string grup)
        {
            switch (grup)
            {
                case "1":
                    grup = "getDetailVehicle";
                    break;
                case "2":
                    grup = "getDetailShip";
                    break;
                case "3":
                    grup = "getDetailAircraft";
                    break;
            }

            var result = await NetworkRequest.GetRequest("http://localhost/eppkm/public/api/" + grup + "?id=" + ID);

            if (result.data == null) return null;
            return result.data;
        }

        public static async Task<string> GetSkenarioAktif()
        {
            var result = await NetworkRequest.PostRequest("http://localhost/eppkm/public/api/getSkenarioAktif", null, enableDebug);

            if (result.data != null)
            {
                try
                {
                    var skenario = SkenarioAktifHelper.FromJson(result.data);
                    new SkenarioAktif(skenario);

                    return "done";

                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }

            return checkRequestResult(result.request);
        }

        public static async Task<string> GetSkenarioAktif2()
        {
            WWWForm form = new WWWForm();
            form.AddField("status", "layer_peta_layer_get_scenario_aktif");
            var result = await NetworkRequest.PostRequest("http://localhost/eppkm_simulasi/docs/source/source_get.php", form, enableDebug);

            if (result.data != null)
            {
                return result.data;
            }

            return checkRequestResult(result.request);
        }

        public static async Task<string> GetAllCB()
        {
            WWWForm form = new WWWForm();

            var result = await NetworkRequest.PostRequest("http://localhost/eppkm/public/api/get/cb/best_rev", form, enableDebug);

            if (result.data != null)
            {
                try
                {
                    var cbTerbaik = CBTerbaikHelper.FromJson(result.data);

                    if (cbTerbaik.Length > 1)
                        foreach (CBTerbaikHelper item in cbTerbaik)
                        {
                            if (item.tipe_cb == "CB Musuh") new CBMusuh(item);
                            await EntityController.instance.LoadEntityFromCB(item.id_user, item.id_kogas, SkenarioAktif.ID_SKENARIO, item.nama_document, 1);
                        }

                    return "done";

                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }

            return checkRequestResult(result.request);
        }

        public static async Task<JArray> loadDataCB(long? id_user, long? id_kogas, long? id_scenario, string nama_document)
        {
            if (!id_kogas.HasValue || !id_kogas.HasValue || !id_scenario.HasValue) return null;

            WWWForm form = new WWWForm();
            form.AddField("status", "ambil_load_dokumen");
            form.AddField("id_user", id_user.Value.ToString());
            form.AddField("nama_dokumen", nama_document);
            form.AddField("skenario", id_scenario.Value.ToString());
            form.AddField("type", id_kogas == 0 ? "menu" : "menu_cb");

            var result = await NetworkRequest.PostRequest("http://localhost/eppkm_simulasi/docs/source/source_get.php", form, enableDebug);

            if (result.data == null) return null;

            return JArray.Parse(result.data);
        }

        private static string checkRequestResult(UnityWebRequest.Result result)
        {
            if (result == UnityWebRequest.Result.ConnectionError)
            {
                return "connection_failed";
            }
            else if (result == UnityWebRequest.Result.ProtocolError)
            {
                return "user_failed";
            }
            else
            {
                return "processing_failed";
            }
        }
    }
}
