using UnityEngine;
using System.Collections;

public interface IBall {

	void Goal(bool sendInfoToServer);

	void BallTouchedPlayer();

	void BallTouchedWall(bool leftPart);
}
