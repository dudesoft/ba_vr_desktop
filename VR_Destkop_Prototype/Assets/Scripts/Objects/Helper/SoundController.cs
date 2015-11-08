using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip delete;
    public AudioClip store;

    public void Delete()
    {
        AudioSource.PlayClipAtPoint(delete, transform.position);
    }

    public void Store()
    {
        AudioSource.PlayClipAtPoint(store, transform.position);
    }
}
