#include "pps.h"

void ppsInit(void)
{
	asm volatile ( "MOV #OSCCON, w1 \n"
	"MOV #0x46, w2 \n"
	"MOV #0x57, w3 \n"
	"MOV.b w2, [w1] \n"
	"MOV.b w3, [w1] \n"
	"BCLR OSCCON,#6");
		// unlock sequence	

/*	RPINR20bits.SDI1R = 5;		// SDCARD SDI @ RP5
	RPOR7bits.RP15R = 8;			// SDCARD SCK @ RP4
	RPOR5bits.RP10R = 7;			// SDCARD SDO @ RP3
	RPOR6bits.RP12R = 9;		// SDCARD CS @ RP12

	CNPU2bits.CN17PUE = 1;		// Enable Pull-up
	CNPU2bits.CN21PUE = 1;		// Enable Pull-up 

	RPOR7bits.RP14R=18;        //PWM1 auf RP14
	RPOR7bits.RP15R=19;        //PWM2 auf RP15
*/
	asm volatile ( "MOV #OSCCON, w1 \n"
	"MOV #0x46, w2 \n"
	"MOV #0x57, w3 \n"
	"MOV.b w2, [w1] \n"
	"MOV.b w3, [w1] \n"
	"BSET OSCCON,#6");
		// lock sequence
}
