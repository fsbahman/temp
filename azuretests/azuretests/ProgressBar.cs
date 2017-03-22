using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuretests
{
    public enum ProgressType
    {
        BarDashType1,
        BarDashType2,
        BarDashType3,
        BarDashType4,
        BarEqual,
        Dot,
        Percentage
    }

    public class ProgressBar
    {
        ProgressType type;
        string prefixText;
        string[] steps;
        int step;

        public ProgressBar(string prefix = "In Progress", ProgressType type = ProgressType.Dot)
        {
            this.prefixText = prefix;
            this.type = type;
            InitSteps(prefix);
        }

        #region Steps Initialization
        
        private void InitSteps(string prefixText = "")
        {
            switch (type)
            {
                case ProgressType.BarDashType1:
                    InitBarDashtType1Steps(prefixText);
                    break;
                case ProgressType.BarDashType2:
                    InitBarDashtType2Steps(prefixText);
                    break;
                case ProgressType.BarDashType3:
                    InitBarDashtType3Steps(prefixText);
                    break;
                case ProgressType.BarDashType4:
                    InitBarDashtType4Steps(prefixText);
                    break;
                case ProgressType.Dot:
                    InitDotSteps(prefixText);
                    break;
                case ProgressType.Percentage:
                    InitPercentageSteps(prefixText);
                    break;
                default:
                    InitDotSteps(prefixText);
                    break;
            }
        }

        private void InitBarDashtType1Steps(string prefixText = "")
        {
            steps = new string[] {
                $"{prefixText} [          ]",
                $"{prefixText} [-         ]",
                $"{prefixText} [--        ]",
                $"{prefixText} [---       ]",
                $"{prefixText} [----      ]",
                $"{prefixText} [-----     ]",
                $"{prefixText} [------    ]",
                $"{prefixText} [-------   ]",
                $"{prefixText} [--------  ]",
                $"{prefixText} [--------- ]",
                $"{prefixText} [----------]"};
        }

        private void InitBarDashtType2Steps(string prefixText = "")
        {
            steps = new string[] {
                $"{prefixText} [-         ]",
                $"{prefixText} [ -        ]",
                $"{prefixText} [  -       ]",
                $"{prefixText} [   -      ]",
                $"{prefixText} [    -     ]",
                $"{prefixText} [     -    ]",
                $"{prefixText} [      -   ]",
                $"{prefixText} [       -  ]",
                $"{prefixText} [        - ]",
                $"{prefixText} [         -]"};
        }

        private void InitBarDashtType3Steps(string prefixText = "")
        {
            steps = new string[] {
                $"{prefixText} [-         ]",
                $"{prefixText} [ -        ]",
                $"{prefixText} [  -       ]",
                $"{prefixText} [   -      ]",
                $"{prefixText} [    -     ]",
                $"{prefixText} [     -    ]",
                $"{prefixText} [      -   ]",
                $"{prefixText} [       -  ]",
                $"{prefixText} [        - ]",
                $"{prefixText} [         -]",
                $"{prefixText} [        - ]",
                $"{prefixText} [       -  ]",
                $"{prefixText} [      -   ]",
                $"{prefixText} [     -    ]",
                $"{prefixText} [    -     ]",
                $"{prefixText} [   -      ]",
                $"{prefixText} [  -       ]",
                $"{prefixText} [ -        ]"};
        }

        private void InitBarDashtType4Steps(string prefixText = "")
        {
            steps = new string[] {
                $"{prefixText} [-         ]",
                $"{prefixText} [--        ]",
                $"{prefixText} [ --       ]",
                $"{prefixText} [  --      ]",
                $"{prefixText} [   --     ]",
                $"{prefixText} [    --    ]",
                $"{prefixText} [     --   ]",
                $"{prefixText} [      --  ]",
                $"{prefixText} [       -- ]",
                $"{prefixText} [        --]",
                $"{prefixText} [         -]"};
        }

        private void InitDotSteps(string prefixText = "")
        {
            steps = new string[] {
                $"{prefixText}   ",
                $"{prefixText}.  ",
                $"{prefixText}.. ",
                $"{prefixText}...",
                $"{prefixText}.. ",
                $"{prefixText}.  ",
                $"{prefixText}   "};
        }

        private void InitTextDotSteps(string prefixText = "")
        {
            steps = new string[] {
                $"{prefixText}   ",
                $"{prefixText}.  ",
                $"{prefixText}.. ",
                $"{prefixText}...",
                $"{prefixText}.. ",
                $"{prefixText}.  ",
                $"{prefixText}   "};
        }

        private void InitPercentageSteps(string prefixText = "")
        {
            steps = new string[] {
                $"{prefixText}   ",
                $"{prefixText}.  ",
                $"{prefixText}.. ",
                $"{prefixText}...",
                $"{prefixText}.. ",
                $"{prefixText}.  ",
                $"{prefixText}   "};
        }
        #endregion

        public void Start()
        {
            step = 0;
            Console.CursorVisible = false;
        }

        public void Pulse(string extraData)
        {
            if (!string.IsNullOrEmpty(extraData))
            {
                InitSteps($"{prefixText} {extraData}");
            }
            int nextLine = Console.CursorTop + 1;
            Utility.WriteAtPosition(
                $"{steps[step]}{new string(' ', Console.WindowWidth - steps[step].Length)}", 1, nextLine, ConsoleColor.Yellow);
            step = (step + 1) % steps.Length;
        }

        public void Pulse()
        {
            Pulse("");
        }

        ~ProgressBar()
        {
            Stop();
        }

        public void Stop()
        {
            int nextLine = Console.CursorTop + 1;
            Utility.WriteAtPosition(new string(' ', steps[0].Length), 1, nextLine, ConsoleColor.Yellow);
            Console.CursorVisible = true;
        }
    }
}
