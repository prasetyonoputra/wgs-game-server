using System;
using System.Threading.Tasks;
using UnityEngine;
using Wargaming.Core.GlobalParam.HelperScenarioAktif;
using Wargaming.Core.Network;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;
    public SkenarioAktifWaktu skenario;
    public DateTime  tanggalMulai, tanggalSelesai, waktuSekarang;
    public float elapsed = 0f, percepatan = 1f;
    public bool isPlaying = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async Task Init()
    {
        var jsonSkenario = await WargamingAPI.GetSkenarioAktif2();
        string x = jsonSkenario.Substring(1);
        string result = x.Remove(x.Length - 1);

        skenario = SkenarioAktifWaktu.FromJson(result);

        tanggalMulai = Convert.ToDateTime(skenario.tgl_mulai_asum);
        tanggalSelesai = Convert.ToDateTime(skenario.tgl_selesai_asum);
        waktuSekarang = tanggalMulai;
    }

    private void Update()
    {
        if (isPlaying)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 1f)
            {
                elapsed %= 1f;
                waktuSekarang = waktuSekarang.AddSeconds(1 * percepatan);
                //Debug.Log(waktuSekarang.ToString());

                ColyseusController.instance.SetTime(waktuSekarang.ToString());
            }
        }
    }

    public DateTimeOffset getDateTimeOffset()
    {
        return waktuSekarang;
    }

    internal void ChangeMedia(string action)
    {
        switch (action)
        {
            case "stop":
                isPlaying = false;
                break;
            case "play":
                isPlaying = true;
                break;
            case "start":
                isPlaying = true;
                break;
            case "resume":
                isPlaying = true;
                break;
            case "pause":
                isPlaying = false;
                break;
        }
    }

    internal void SetPercepatan(int v)
    {
        percepatan = v;
    }
}
