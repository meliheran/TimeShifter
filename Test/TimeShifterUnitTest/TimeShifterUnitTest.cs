using meliheran;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TimeShifterUnitTest
{
    [TestClass]
    public class TimeShifterUnitTest
    {
        string _sampleText;

        [TestMethod]
        public void TestShifting()
        {
            TimeShifter<string> shifter = new TimeShifter<string>(3000,200);            //Every 200 period shifting time will be checked that 3 sec passed 
            shifter.OnEndShiftingTime += (string obj) => { _sampleText = obj; };        //When there are no intterruption since 3 secs event fired
            shifter.Shift("a");                                                         //User interrupts with typing 'a' and shifting will postpone 3 sec
            shifter.Shift("ab");                                                        //User interrupts with typing 'b' and shifting time will reset and postpone 3 sec
            System.Threading.Thread.Sleep(2000);                                        //Event idle time 2 sec no event fired
            shifter.Shift("abc");                                                       //Almost 1 sec left to fire event. But user interrupts and reset the time so shiting postpone 3 sec again
            System.Threading.Thread.Sleep(1500);
            shifter.Shift("abcd");

            Assert.AreNotEqual<string>(_sampleText, "abcd");                            //Shifting event never fired until 3 sec passed
            System.Threading.Thread.Sleep(4000);                                        //After 3 sec idle time with a precision 200 ms event will be fired and time to do whatever
            Assert.AreEqual<string>(_sampleText, "abcd");                               //Event returns the last objet for ex: Textbox.Text after shifting time passed.
                                                                                        //In an other way wait for the user x seconds for idle to make a request to db. Or reset the timer when shift method called by the user on keypress or some other action
        }
    }
}
