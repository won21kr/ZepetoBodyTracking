using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct AlbodyStruct
{
    public int id;
    public List<float[]> kpt2d_body;
    public List<float[]> kpt3d_body;
    public AlbodyStruct(int _id, List<float[]> _kpt2d_body, List<float[]> _kpt3d_body)
    {
        id = _id;
        kpt2d_body = _kpt2d_body;
        kpt3d_body = _kpt3d_body;
    }
}
public struct body
{
    public List<float[]> kpt2d_body;
    public List<float[]> kpt3d_body;
}
public struct worldPos
{
    public List<float[]> world_pos;
}