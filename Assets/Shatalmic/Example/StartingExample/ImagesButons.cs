using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImagesButons : MonoBehaviour
{
    public string DeviceName = "MESA_COGNITIVA";
    public string ServiceUUID = "A9E90000-194C-4523-A473-5FDF36AA4D20";
    public string ButtonUUID = "A9E90002-194C-4523-A473-5FDF36AA4D20";

    enum States
    {
        None,
        Scan,
        ScanRSSI,
        ReadRSSI,
        Connect,
        RequestMTU,
        Subscribe,
        Unsubscribe,
        Disconnect,
    }

    private bool _connected = false;
    private float _timeout = 0f;
    private States _state = States.None;
    private string _deviceAddress;
    private bool _foundButtonUUID = false;
    private bool _rssiOnly = false;
    private int _rssi = 0;

    public Text StatusText;

   public TextMeshProUGUI PositionText;
   /* public Text ButtonPositionText1;
    public Text ButtonPositionText2;
    public Text ButtonPositionText3;
    public Text ButtonPositionText4;
    public Text ButtonPositionText5;
    public Text ButtonPositionText6;
*/
    public GameObject img1;
    public GameObject img2;
    public GameObject img3;
    public GameObject img4;
    public GameObject img5;
    public GameObject img6;


    private string StatusMessage
    {
        set
        {
            BluetoothLEHardwareInterface.Log(value);
            StatusText.text = value;
        }
    }

    void Reset()
    {
        _connected = false;
        _timeout = 0f;
        _state = States.None;
        _deviceAddress = null;
        _foundButtonUUID = false;


        _rssi = 0;
    }

    void SetState(States newState, float timeout)
    {
        _state = newState;
        _timeout = timeout;
    }

    void StartProcess()
    {
        Reset();
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {

            SetState(States.Scan, 0.1f);

        }, (error) =>
        {

            StatusMessage = "Error during initialize: " + error;
        });
    }

    // Use this for initialization
    void Start()
    {
        StartProcess();
    }

    private void ProcessButton(byte[] bytes)
    {
        string texto = " ";
        for(int i=0; i<bytes.Length; i++)
        {
            texto = texto + bytes[i].ToString();
        }
        PositionText.GetComponent<TextMeshProUGUI>().text = texto;

        /*if (bytes[0] == 0x00)
         {
            // ButtonPositionText.text = "But1: OFF " + bytes[0];
             img1.gameObject.SetActive(false);
             img2.gameObject.SetActive(false);
             img3.gameObject.SetActive(false);
             img4.gameObject.SetActive(false);
             img5.gameObject.SetActive(false);
             img6.gameObject.SetActive(false);
         }

         else if (bytes[0] == 0x01)
         {
             img2.gameObject.SetActive(false);
             img3.gameObject.SetActive(false);
             img4.gameObject.SetActive(false);
             img5.gameObject.SetActive(false);
             img6.gameObject.SetActive(false);
             img1.gameObject.SetActive(true);
         }

         if (bytes[1] == 0x00)
         {

             //  ButtonPositionText1.text = "But2: OFF " + bytes[1];
             img1.gameObject.SetActive(false);
             img2.gameObject.SetActive(false);
             img3.gameObject.SetActive(false);
             img4.gameObject.SetActive(false);
             img5.gameObject.SetActive(false);
             img6.gameObject.SetActive(false);
         }
         else if (bytes[1] == 0x01)
         {
             img1.gameObject.SetActive(false);
             img3.gameObject.SetActive(false);
             img4.gameObject.SetActive(false);
             img5.gameObject.SetActive(false);
             img6.gameObject.SetActive(false);
             img2.gameObject.SetActive(true);
         }

         if (bytes[2] == 0x00)
         {
             img1.gameObject.SetActive(false);
             img2.gameObject.SetActive(false);
             img3.gameObject.SetActive(false);
             img4.gameObject.SetActive(false);
             img5.gameObject.SetActive(false);
             img6.gameObject.SetActive(false);
         }

         else if (bytes[2] == 0x01)
         {
             img1.gameObject.SetActive(false);
             img2.gameObject.SetActive(false);
             img4.gameObject.SetActive(false);
             img5.gameObject.SetActive(false);
             img6.gameObject.SetActive(false);
             img3.gameObject.SetActive(true);

         }


         if (bytes[3] == 0x00)
         {
             img1.gameObject.SetActive(false);
             img2.gameObject.SetActive(false);
             img3.gameObject.SetActive(false);
             img4.gameObject.SetActive(false);
             img5.gameObject.SetActive(false);
             img6.gameObject.SetActive(false);
         }

         else if (bytes[3] == 0x01)
         {
             img1.gameObject.SetActive(false);
             img2.gameObject.SetActive(false);
             img3.gameObject.SetActive(false);
             img5.gameObject.SetActive(false);
             img6.gameObject.SetActive(false); 
             img4.gameObject.SetActive(true);
         }

         if (bytes[4] == 0x00)
         {
             img1.gameObject.SetActive(false);
             img2.gameObject.SetActive(false);
             img3.gameObject.SetActive(false);
             img4.gameObject.SetActive(false);
             img5.gameObject.SetActive(false);
             img6.gameObject.SetActive(false);
         }

         else if (bytes[4] == 0x01)
         {
             img1.gameObject.SetActive(false);
             img2.gameObject.SetActive(false);
             img3.gameObject.SetActive(false);
             img4.gameObject.SetActive(false);
             img6.gameObject.SetActive(false);
             img5.gameObject.SetActive(true);
         }

         if (bytes[5] == 0x00)
         {
             img1.gameObject.SetActive(false);
             img2.gameObject.SetActive(false);
             img3.gameObject.SetActive(false);
             img4.gameObject.SetActive(false);
             img5.gameObject.SetActive(false);
             img6.gameObject.SetActive(false);
         }
         else if (bytes[5] == 0x01)
         {
             img1.gameObject.SetActive(false);
             img2.gameObject.SetActive(false);
             img3.gameObject.SetActive(false);
             img4.gameObject.SetActive(false);
             img5.gameObject.SetActive(false);
             img6.gameObject.SetActive(true);
         }*/


    }


    public void ManagePress()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (_timeout > 0f)
        {
            _timeout -= Time.deltaTime;
            if (_timeout <= 0f)
            {
                _timeout = 0f;

                switch (_state)
                {
                    case States.None:
                        break;

                    case States.Scan:
                        StatusMessage = "Scanning for " + DeviceName;

                        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
                        {
                            // if your device does not advertise the rssi and manufacturer specific data
                            // then you must use this callback because the next callback only gets called
                            // if you have manufacturer specific data

                            if (!_rssiOnly)
                            {
                                if (name.Contains(DeviceName))
                                {
                                    StatusMessage = "1.Found " + name;

                                    // found a device with the name we want
                                    // this example does not deal with finding more than one
                                    _deviceAddress = address;
                                    SetState(States.Connect, 0.5f);
                                }
                            }

                        }, (address, name, rssi, bytes) =>
                        {

                            // use this one if the device responses with manufacturer specific data and the rssi

                            if (name.Contains(DeviceName))
                            {
                                StatusMessage = "2.Found " + name;

                                if (_rssiOnly)
                                {
                                    _rssi = rssi;
                                }
                                else
                                {
                                    // found a device with the name we want
                                    // this example does not deal with finding more than one
                                    _deviceAddress = address;
                                    SetState(States.Connect, 0.5f);
                                }
                            }

                        }, _rssiOnly); // this last setting allows RFduino to send RSSI without having manufacturer data

                        if (_rssiOnly)
                            SetState(States.ScanRSSI, 0.5f);
                        break;

                    case States.ScanRSSI:
                        break;

                    case States.ReadRSSI:
                        StatusMessage = $"Call Read RSSI";
                        BluetoothLEHardwareInterface.ReadRSSI(_deviceAddress, (address, rssi) =>
                        {
                            StatusMessage = $"Read RSSI: {rssi}";
                        });

                        BluetoothLEHardwareInterface.ReadCharacteristic(_deviceAddress, ServiceUUID, ButtonUUID, (characteristic, bytes) =>
                        {
                            ProcessButton(bytes);
                            StatusMessage = "process button";
                        });


                        SetState(States.ReadRSSI, 2f);
                        break;

                    case States.Connect:
                        StatusMessage = "Connecting...";

                        // set these flags
                        _foundButtonUUID = false;

                        //_foundLedUUID = false;

                        // note that the first parameter is the address, not the name. I have not fixed this because
                        // of backwards compatiblity.
                        // also note that I am note using the first 2 callbacks. If you are not looking for specific characteristics you can use one of
                        // the first 2, but keep in mind that the device will enumerate everything and so you will want to have a timeout
                        // large enough that it will be finished enumerating before you try to subscribe or do any other operations.
                        BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceAddress, null, null, (address, serviceUUID, characteristicUUID) =>
                        {
                            StatusMessage = "Connected...";

                            BluetoothLEHardwareInterface.StopScan();

                            if (IsEqual(serviceUUID, ServiceUUID))
                            {
                                StatusMessage = "Found Service UUID";

                                _foundButtonUUID = _foundButtonUUID || IsEqual(characteristicUUID, ButtonUUID);



                                // if we have found both characteristics that we are waiting for
                                // set the state. make sure there is enough timeout that if the
                                // device is still enumerating other characteristics it finishes
                                // before we try to subscribe

                                if (_foundButtonUUID)
                                {
                                    _connected = true;
                                    SetState(States.RequestMTU, 4f);
                                    StatusMessage = "Connected";
                                }
                            }
                        });

                        break;

                    case States.RequestMTU:
                        StatusMessage = "Requesting MTU";

                        BluetoothLEHardwareInterface.RequestMtu(_deviceAddress, 185, (address, newMTU) =>
                        {
                            StatusMessage = "MTU set to " + newMTU.ToString();

                            SetState(States.Subscribe, 0.1f);
                        });
                        break;

                    case States.Subscribe:
                        StatusMessage = "Subscribing to characteristics...";

                        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceAddress, ServiceUUID, ButtonUUID, (notifyAddress, notifyCharacteristic) =>
                        {
                            StatusMessage = "Waiting for user action (1)...";
                            _state = States.None;

                            // read the initial state of the button
                            BluetoothLEHardwareInterface.ReadCharacteristic(_deviceAddress, ServiceUUID, ButtonUUID, (characteristic, bytes) =>
                            {
                                ProcessButton(bytes);
                                StatusMessage = "process button";
                            });

                            SetState(States.ReadRSSI, 1f);

                        }, (address, characteristicUUID, bytes) =>
                        {
                            if (_state != States.None)
                            {
                                // some devices do not properly send the notification state change which calls
                                // the lambda just above this one so in those cases we don't have a great way to
                                // set the state other than waiting until we actually got some data back.
                                // The esp32 sends the notification above, but if yuor device doesn't you would have
                                // to send data like pressing the button on the esp32 as the sketch for this demo
                                // would then send data to trigger this.
                                StatusMessage = "Waiting for user action (2)...";

                                SetState(States.ReadRSSI, 1f);
                            }

                            // we received some data from the device
                            ProcessButton(bytes);
                            StatusMessage = " Recieved Data";
                        });


                        break;

                    case States.Unsubscribe:
                        BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_deviceAddress, ServiceUUID, ButtonUUID, null);
                        SetState(States.Disconnect, 4f);
                        break;

                    case States.Disconnect:
                        StatusMessage = "Commanded disconnect.";

                        if (_connected)
                        {
                            BluetoothLEHardwareInterface.DisconnectPeripheral(_deviceAddress, (address) =>
                            {
                                StatusMessage = "Device disconnected";
                                BluetoothLEHardwareInterface.DeInitialize(() =>
                                {
                                    _connected = false;
                                    _state = States.None;
                                });
                            });
                        }
                        else
                        {
                            BluetoothLEHardwareInterface.DeInitialize(() =>
                            {
                                _state = States.None;
                            });
                        }
                        break;
                }
            }
        }
    }

    // private bool ledON = false;
    /* public void OnLED()
     {
         ledON = !ledON;
         if (ledON)
         {
             SendByte((byte)0x01);
         }
         else
         {
             SendByte((byte)0x00);
         }
     }
    */
    string FullUUID(string uuid)
    {
        string fullUUID = uuid;
        if (fullUUID.Length == 4)
            fullUUID = "0000" + uuid + "-0000-1000-8000-00805f9b34fb";

        return fullUUID;
    }

    bool IsEqual(string uuid1, string uuid2)
    {
        if (uuid1.Length == 4)
            uuid1 = FullUUID(uuid1);
        if (uuid2.Length == 4)
            uuid2 = FullUUID(uuid2);

        return (uuid1.ToUpper().Equals(uuid2.ToUpper()));
    }
}
