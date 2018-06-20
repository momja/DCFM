using UnityEngine;
using LevelGenerator;

public class TestLevelGenerator : MonoBehaviour {
    public void Start() {
        LevelGenerator.LevelGenerator gen = new LevelGenerator.LevelGenerator(50);
    }
}