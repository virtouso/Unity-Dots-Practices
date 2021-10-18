
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


public class EcsManager : MonoBehaviour
{

    [HideInInspector] public static EntityManager EntityManager;
    [SerializeField] public GameObject VirusPrefab;
    [SerializeField] public GameObject BulletPrefab;
    [SerializeField] private int _virusNumber = 500;
    [SerializeField] private int _bulletNumber;
    private BlobAssetStore _store;


    private Entity bullet;

    private void Start()
    {
        _store = new BlobAssetStore();
        EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _store);
        Entity virus = GameObjectConversionUtility.ConvertGameObjectHierarchy(VirusPrefab, settings);
        bullet = GameObjectConversionUtility.ConvertGameObjectHierarchy(BulletPrefab, settings);

        for (int i = 0; i < _virusNumber; i++)
        {
            var instance = EntityManager.Instantiate(virus);
            float x = UnityEngine.Random.Range(-50, 50);
            float y = UnityEngine.Random.Range(-50, 50);
            float z = UnityEngine.Random.Range(-50, 50);
            float3 position = new float3(x, y, z);
            EntityManager.SetComponentData(instance, new Translation { Value = position });
            float rspeed = UnityEngine.Random.Range(-10, 10);

            EntityManager.SetComponentData(instance, new FloatData { Speed = rspeed });
        }

        Debug.Log("asdsdaa");
    }




    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < _bulletNumber; i++)
            {

                var instance = EntityManager.Instantiate(bullet);
                var startPos = Camera.main.transform.position;
                EntityManager.SetComponentData(instance, new Translation { Value = startPos });

                EntityManager.SetComponentData(instance, new Rotation { Value = Camera.main.transform.rotation });


            }
        }
    }


    private void OnDestroy()
    {
        _store.Dispose();
    }



}