using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Disk
{
    public GameObject ufo;
    public Material material;
    public int score;
    private int base_score=100;

    public Disk(GameObject _ufo,Material _mat)
    {
        ufo = _ufo;
        material = _mat;

        int w_ufo = ufo.name[4]-'0';
        int w_mat = material.name[5]-'0';
        score = w_mat * w_ufo * base_score;
    }
}
