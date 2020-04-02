#include "hardware.h"	// include file fo Controller
#include "PPS/pps.h"	// peripheral pin select
#include "peripherals/adc/adc.h" // adc 
#include "peripherals/timer/timer.h" // timer
#include "usb stack/include/usb.h"
#include "usb stack/include/usb_function_cdc.h"

_CONFIG1(	WDTPS_PS1 & /* 1:1 */ \
			FWPSA_PR32 & /* 1:32 */ \
			WINDIS_OFF & /* Standard Watchdog Timer is enabled */ \
			FWDTEN_OFF & /* Watchdog Timer is disabled */ \
			ICS_PGx1 & /* Emulator functions are shared with PGEC1/PGED3 */ \
			GWRP_OFF & /* Writes to program memory are disabled */ \
			GCP_OFF & /* Code protection is disabled */ \
			JTAGEN_OFF); /* JTAG port is Disabled */ \
			
_CONFIG2(	POSCMOD_HS & /* HS Oscillator mode selected */ \
			I2C1SEL_PRI & /* Use default SCL1/SDA1 pins for I2C1 */ \
			IOL1WAY_OFF & /* The IOLOCK bit can be set and cleared using the unlock sequence */ \
			OSCIOFNC_ON & /* OSCO pin functions as port I/O (RA3) */ \
			FCKSM_CSDCMD & /* Sw Disabled, Mon Disabled */ \
			FNOSC_PRIPLL & /* Primary Oscillator with PLL module (XTPLL, HSPLL, ECPLL) */ \
			PLL96MHZ_ON & /* 96 MHz PLL Startup is enabled automatically on start-up */ \
			PLLDIV_DIV4 & /* Oscillator input divided by 4 (16 MHz input) */ \
			IESO_ON); /* IESO mode (Two-Speed Start-up) enabled */ \
			
_CONFIG3(	WPFP_WPFP0 & /* Page 0 (0x0) */ \
			SOSCSEL_IO & /* SOSC pins have digital I/O functions (RA4, RB4) */ \
			WUTSEL_LEG & /* Default regulator start-up time used */ \
			WPDIS_WPDIS & /* Segmented code protection disabled */ \
			WPCFG_WPCFGDIS & /* Last page and Flash Configuration words are unprotected */ \
			WPEND_WPENDMEM); /* Write Protect from page 0 to WPFP */ \
			
_CONFIG4(	DSWDTPS_DSWDTPS3 & /* 1:128 (132 ms) */ \
			DSWDTOSC_LPRC & /* DSWDT uses Low Power RC Oscillator (LPRC) */ \
			RTCOSC_SOSC & /* RTCC uses Low Power RC Oscillator (LPRC) */ \
			DSBOREN_OFF & /* BOR disabled in Deep Sleep */ \
			DSWDTEN_OFF); /* DSWDT disabled */ \

extern volatile struct sAdc adc;

int main (void)
{
	char str_buffer[32];

	_TRISINIT;	
    _ANSINIT;
    _LATINIT;

	USBInitializeSystem();		// init USB subsystem
	USBDeviceAttach();			// force device attach (because we use interrupt based usb)
	
	ppsInit();					// init hardware mapping
	adcInit(); 					// init ADC Conversion
	timer1Init(); 				// init timer 1
	timer2Init(); 				// init timer 2
	
	while(1)
	{
			if((USBDeviceState < ADR_PENDING_STATE) || (USBSuspendControl==1))		// no USB link active == normal mode
		{
			
		}	
		else if (USBDeviceState < CONFIGURED_STATE || (USBSuspendControl==1))	// USB enumeration in progress
		{
			
		}	
		else		// USB connected and enumerated
		{		
   
   			sprintf(str_buffer, "%04d;%04d;%04d;\r\n", adc.data[0], adc.data[1],adc.data[2]); //convert byte into string array
   
  			putsUSBUSART(str_buffer);

	    	CDCTxService();
		}
		
		if(adc.sampling == 0) //reset sampling
		{
			adc.sampling = 1;
		} 
		// common parts
	}
	return 1;
}
