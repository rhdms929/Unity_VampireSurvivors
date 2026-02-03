using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  근접 무기
public class MeleeWeaponStrategy : IWeaponAbility
{
    public void Initialize(WeaponManager manager)
    {
        manager.speed = 150f; // 근접 무기 전용 속도
        Batch(manager);
    }

    public void Execute(WeaponManager manager)
    {
        // 기존 case 0의 회전 로직
        manager.transform.Rotate(Vector3.back * manager.speed * Time.deltaTime);
    }

    public void OnLevelUp(WeaponManager manager)
    {
        Batch(manager);
    }

    // 기존 Batch 함수를 전략 안으로 이동
    private void Batch(WeaponManager manager)
    {
        for (int i = 0; i < manager.count; i++)
        {
            Transform weapon;
            if (i < manager.transform.childCount)
            {
                weapon = manager.transform.GetChild(i);
            }
            else
            {
                weapon = GameManager.instance.pool.Get(manager.prefabId).transform;
                weapon.parent = manager.transform;
            }

            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / manager.count;
            weapon.Rotate(rotVec);
            weapon.Translate(weapon.up * 1.5f, Space.World);

            // -1은 무한 관통/공격
            weapon.GetComponent<Weapon>().Init(manager.damage, -1, Vector3.zero);
        }
    }

    // 원거리 공격
//    using UnityEngine;

//public class RangedWeaponStrategy : IWeaponStrategy
//{
//    private float _timer;

//    public void Initialize(WeaponManager manager)
//    {
//        manager.speed = 0.3f; // 원거리 무기 기본 발사 속도
//    }

//    public void Execute(WeaponManager manager)
//    {
//        // 기존 Update의 default 케이스 로직
//        _timer += Time.deltaTime;

//        if (_timer > manager.speed)
//        {
//            _timer = 0f;
//            Fire(manager);
//        }
//    }

//    public void OnLevelUp(WeaponManager manager)
//    {
//        // 원거리는 레벨업 시 별도의 배치(Batch)가 필요 없으므로 비워둡니다.
//        // 필요하다면 데미지 로그 등을 찍을 수 있습니다.
//    }

//    private void Fire(WeaponManager manager)
//    {
//        // 기존 WeaponManager의 Fire 로직 그대로 이동
//        if (!manager.player.scanner.nearestTarget)
//            return;

//        Vector3 targetPos = manager.player.scanner.nearestTarget.position;
//        Vector3 dir = (targetPos - manager.transform.position).normalized;

//        Transform bullet = GameManager.instance.pool.Get(manager.prefabId).transform;
//        bullet.position = manager.transform.position;
//        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

//        // 원거리는 count를 넘겨서 관통 횟수 등을 제어
//        bullet.GetComponent<Weapon>().Init(manager.damage, manager.count, dir);
//    }
//}
}
