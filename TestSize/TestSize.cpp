// TestSize.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

typedef struct _WacomMTFinger
{
	int							FingerID;
	float							X;
	float							Y;
	float							Width;
	float							Height;
	unsigned short				Sensitivity;
	float							Orientation;
	bool							Confidence;
} WacomMTFinger;

typedef struct _test
{
	char c;
	int							FingerID;
} Test;

typedef struct _WacomMTFingerCollection
{
	int							Version;
	int							DeviceID;
	int							FrameNumber;
	int							FingerCount;
	WacomMTFinger				*Fingers;
} WacomMTFingerCollection;

int _tmain(int argc, _TCHAR* argv[])
{
	WacomMTFinger fingers[4]= {};
	for(int i=0;i<4;i++)
	{
		fingers[i].Confidence = false;
		fingers[i].FingerID = i;
		fingers[i].X = i;
		fingers[i].Y = i;
		fingers[i].Width = i;
		fingers[i].Height =i;
		fingers[i].Sensitivity = i;
		fingers[i].Orientation = i;
	}
	WacomMTFinger *p;
	p = fingers;
	printf("%d \n",sizeof(*p));
	
	WacomMTFingerCollection a;
	a.Fingers = fingers;
	printf("%d",sizeof(a));

	return 0;
}

