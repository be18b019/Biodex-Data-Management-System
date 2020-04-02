#ifndef __HRDW_H
#include "../../hardware.h"
#endif

void adcInit(void);

struct sAdc
{
	unsigned int data[3]; //collected data
	unsigned int temp[3]; //for oversampling
	unsigned int cnt; //cnt for oversampling
	unsigned char sampling; //bool for sampling
};	
