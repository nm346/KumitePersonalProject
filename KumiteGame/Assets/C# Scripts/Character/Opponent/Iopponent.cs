using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iopponent{
	void Execute();
	void Enter (Opponent opponent);
	void Exit ();
	void OnTriggerEnter(Collider2D other);

}
