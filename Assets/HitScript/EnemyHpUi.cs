using UnityEngine;
using UnityEngine.UI;

public class EnemyHpUi : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    // HPデータを更新するメソッドを追加
    public void UpdateData(EnemyPresentationData data)
    {
        if (hpSlider != null)
        {
            hpSlider.value = (float)data.EnemyData.CurrentHp / data.EnemyData.MaxHp;
        }
    }
}