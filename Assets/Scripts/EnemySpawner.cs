using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private AnimationCurve _distribution;
    [SerializeField] private GameObject[] _objectsToSpawn;

    [Header("Time")] 
    [SerializeField, Tooltip("In seconds.")] private float _timeBeforeFirstSpawn;
    [SerializeField] private float _timeBetweenSpawns;

    private Transform _transform;
    private Coroutine _spawnRoutine;

    private IEnumerator SpawnObjects() {
        yield return new WaitForSeconds(_timeBeforeFirstSpawn);

        while (true) {
            CreateObject();
            yield return new WaitForSeconds(_timeBetweenSpawns);
        }

        void CreateObject() {
            GameObject go = _objectsToSpawn[0];
            Instantiate(go, _transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
    }

    private void OnEnable() {
        _spawnRoutine = StartCoroutine(SpawnObjects());
    }

    private void OnDisable() {
        StopCoroutine(_spawnRoutine);
    }

    private void Awake() {
        _transform = transform;
    }
}
