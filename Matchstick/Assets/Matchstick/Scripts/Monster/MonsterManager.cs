using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
	public GameObject Monster;
	private MonsterController monsterController;
	private Transform player;
	// �o���Ԋu[s]
	public float MinAppearTime;
	public float MaxAppearTime;
	//
	public List<Vector3> popList;

	private float appearWaitTime;

	private void Awake()
	{
		popList = new List<Vector3>();
		for (int i = 0; i < transform.childCount; i++)
		{
			popList.Add(transform.GetChild(i).position);
		}
		popList.Sort((Vector3 a, Vector3 b) => (int)(a.x - b.x));
		player = GameObject.Find("Player").transform;
	}

	void Start()
    {
		Monster = (GameObject)Instantiate(Monster, Vector3.zero ,Quaternion.identity);
		monsterController = Monster.transform.GetComponent<MonsterController>();

		// �ҋ@���Ԃ����Z�b�g
		appearWaitTime = Random.Range(MinAppearTime, MaxAppearTime);
	}

    void Update()
    {
		if (monsterController.isMove == false)
		{
			if (appearWaitTime > 0)
			{
				appearWaitTime -= Time.deltaTime;
			}
		}
	}

	private void FixedUpdate()
	{
		if (monsterController.isMove == false)
		{
			if(appearWaitTime < 0)
			{
				appearWaitTime = Random.Range(MinAppearTime, MaxAppearTime);

				int i;
				var v = new List<Vector2>();
				for (i = 0; i < popList.Count; i++)
				{
					if (player.position.x < popList[i].x)
					{
						v.Add(popList[i]);
						break;
					}
				}
				// �v���C���[����ɏo���ʒu���Ȃ�
				if(i == popList.Count) { v.Add(popList[i - 1]); }
				// �v���C���[����ɏo���ʒu����
				else if(i != 0) { v.Add(popList[i - 1]); }

				monsterController.StartMove( v[Random.Range(0, v.Count)] );
			}
		}
	}
}
