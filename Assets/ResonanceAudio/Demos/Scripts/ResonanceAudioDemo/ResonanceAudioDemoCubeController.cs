using UnityEngine;

/// Resonance Audio demo cube controller.
[RequireComponent(typeof(Renderer))]
public class ResonanceAudioDemoCubeController : MonoBehaviour {
  // Visual material of the cube.
  private Material material = null;

  void Start() {
    material = GetComponent<Renderer>().material;
    SetGazedAt(false);
  }

  /// 色変更
  public void SetGazedAt(bool gazedAt) {
    material.color = gazedAt ? Color.green : Color.red;
  }

  //キューブ位置をランダムに生成する
  public void TeleportRandomly() {
    Vector3 direction = Random.onUnitSphere;
    direction.y = Mathf.Clamp(direction.y, 0.5f, 1.0f);
    float distance = 2.0f * Random.value + 1.5f;
    transform.localPosition = distance * direction;
  }
}
