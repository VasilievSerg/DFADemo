public class Main {
    public static void TaskAttachTest1(boolean... conditions) {
        int x;
        x = 1;
        if (conditions[0]) {
            x = 2;
            if (conditions[1]) {
                x = 3;
            }
            x = 4;
            if (conditions[2]) {
                x = 5;
            }
        }
        if (conditions[3]) {
            x = 6;
        }
		if (conditions[4]) {
			x = 7;
		}
		if (conditions[5]) {
			x = 8;
		}
		if (conditions[6]) {
			x = 9;
		}
		if (conditions[7]) {
			x = 10;
		}
		if (conditions[8]) {
			x = 11;
		}
		if (conditions[9]) {
			x = 12;
		}
		if (conditions[10]) {
			x = 13;
		}
		if (conditions[11]) {
			x = 14;
		}
		if (conditions[12]) {
			x = 15;
		}
		if (conditions[13]) {
			x = 16;
		}
		if (conditions[14]) {
			x = 17;
		}
		if (conditions[15]) {
			x = 18;
		}
		if (conditions[16]) {
			x = 19;
		}
		if (conditions[17]) {
			x = 20;
		}
		if (conditions[18]) {
			x = 21;
		}
		if (conditions[19]) {
			x = 22;
		}
		if (conditions[20]) {
			x = 23;
		}
		if (conditions[21]) {
			x = 24;
		}
		if (conditions[22]) {
			x = 25;
		}
		if (conditions[23]) {
			x = 26;
		}
		if (conditions[24]) {
			x = 27;
		}
		if (conditions[25]) {
			x = 28;
		}
		if (conditions[26]) {
			x = 29;
		}
		if (conditions[27]) {
			x = 30;
		}
		if (conditions[28]) {
			x = 31;
		}
		if (conditions[29]) {
			x = 32;
		}
		if (conditions[30]) {
			x = 33;
		}
		if (conditions[31]) {
			x = 34;
		}
		if (conditions[32]) {
			x = 35;
		}
		if (conditions[33]) {
			x = 36;
		}
		if (conditions[34]) {
			x = 37;
		}
		if (conditions[35]) {
			x = 38;
		}
		if (conditions[36]) {
			x = 39;
		}
		if (conditions[37]) {
			x = 40;
		}
		if (conditions[38]) {
			x = 41;
		}
		if (conditions[39]) {
			x = 42;
		}

        System.out.println(x);
    }
    
    public static void TaskAttachTest2(boolean... conditions) {
        int x;
        x = 1;
        
        if (conditions[0]) {
            x = 2;
            if (conditions[1]) {
                x = 3;
            }
            x = 4;
            if (conditions[2]) {
                x = 5;
            }
        }
        if (conditions[3]) {
            x = 6;
        }
        System.out.println(x);
    }

    // x: [1, 4, 5, 6]
    public static void TestMethod0(boolean... conditions) {
        int x;
        x = 1;
        if (conditions[0]) {
            x = 2;
            if (conditions[1]) {
                x = 3;
            }
            x = 4;
            if (conditions[2]) {
                x = 5;
            }
        }
        if (conditions[3]) {
            x = 6;
        }
		
        System.out.println(x);
    }
    
    // x: [1]
    public static void TestMethod1(boolean... conditions) {
        int x;
        x = 1;
        
        System.out.println(x);
    }
    
    // x: [1, 2]
    public static void TestMethod2(boolean... conditions) {
        int x;
        x = 1;
        if (conditions[0]) {
            x = 2;
        }
        
        System.out.println(x);
    }
    
    // x: [3]
    public static void TestMethod3(boolean... conditions) {
        int x;
        x = 1;
        if (conditions[0]) {
            x = 2;
        }
        
        x = 3;
        
        System.out.println(x);
    }
    
    // x: [3, 62]
    public static void TestMethod4(boolean... conditions) {
        int x;
        x = 1;
        if (conditions[0]) {
            x = 2;
        }
        
        x = 3;
        if (conditions[1]) {
            x = 62;
        }
        
        System.out.println(x);
    }
    
    // x: [5]
    public static void TestMethod5(boolean... conditions) {
        int x;
        x = 1;
        if (conditions[0]) {
            x = 2;
        }
        
        x = 3;
        if (conditions[1]) {
            x = 62;
        }
        
        x = 5;
        
        System.out.println(x);
    }
    
    // x: [1, 62]
    public static void TestMethod6(boolean... conditions) {
        int x;
        x = 1;
        if (conditions[0]) {
           if (conditions[1]) {
               if (conditions[2]) {
                   x = 62;
               }
           }
        }
        
        System.out.println(x);
    }
    
    // x: [1, 4, 5, 6]
    public static void TestMethod7(boolean... conditions) {
        int x; 
        x = 1;
        
        if (conditions[0]) {
            x = 2;
            if (conditions[1]) {
                x = 3;
            }
            
            x = 4;
            if (conditions[2]) {
                x = 5;
            }
        }
        
        if (conditions[3]) {
            x = 6;
        }
        
        System.out.println(x);
    }
    
    // x: [1, 4, 5, 6 .. 42]
    public static void TestMethod8(boolean... conditions) {
        int x;
        x = 1;
        if (conditions[0]) {
            x = 2;
            if (conditions[1]) {
                x = 3;
            }
            x = 4;
            if (conditions[2]) {
                x = 5;
            }
        }
        if (conditions[3]) {
            x = 6;
        }
        if (conditions[4]) {
            x = 7;
        }
        if (conditions[5]) {
            x = 8;
        }
        if (conditions[6]) {
            x = 9;
        }
        if (conditions[7]) {
            x = 10;
        }
        if (conditions[8]) {
            x = 11;
        }
        if (conditions[9]) {
            x = 12;
        }
        if (conditions[10]) {
            x = 13;
        }
        if (conditions[11]) {
            x = 14;
        }
        if (conditions[12]) {
            x = 15;
        }
        if (conditions[13]) {
            x = 16;
        }
        if (conditions[14]) {
            x = 17;
        }
        if (conditions[15]) {
            x = 18;
        }
        if (conditions[16]) {
            x = 19;
        }
        if (conditions[17]) {
            x = 20;
        }
        if (conditions[18]) {
            x = 21;
        }
        if (conditions[19]) {
            x = 22;
        }
        if (conditions[20]) {
            x = 23;
        }
        if (conditions[21]) {
            x = 24;
        }
        if (conditions[22]) {
            x = 25;
        }
        if (conditions[23]) {
            x = 26;
        }
        if (conditions[24]) {
            x = 27;
        }
        if (conditions[25]) {
            x = 28;
        }
        if (conditions[26]) {
            x = 29;
        }
        if (conditions[27]) {
            x = 30;
        }
        if (conditions[28]) {
            x = 31;
        }
        if (conditions[29]) {
            x = 32;
        }
        if (conditions[30]) {
            x = 33;
        }
        if (conditions[31]) {
            x = 34;
        }
        if (conditions[32]) {
            x = 35;
        }
        if (conditions[33]) {
            x = 36;
        }
        if (conditions[34]) {
            x = 37;
        }
        if (conditions[35]) {
            x = 38;
        }
        if (conditions[36]) {
            x = 39;
        }
        if (conditions[37]) {
            x = 40;
        }
        if (conditions[38]) {
            x = 41;
        }
        if (conditions[39]) {
            x = 42;
        }

        System.out.println(x);
    }
    
    // x: [1, 62]
    public static void TestMethod9(boolean... conditions) {
        int x;
        x = 1;
        
        if (conditions[0])
            if (conditions[1])
                if (conditions[2])
                    if (conditions[3])
                        if (conditions[4])
                            if (conditions[5])
                                if (conditions[6])
                                    if (conditions[7])
                                        if (conditions[8])
                                            if (conditions[9])
                                                if (conditions[10])
                                                    x = 62;
                
        
        System.out.println(x);
    }
    
    // x: [1, 62]
    public static void TestMethod10(boolean... conditions) {
        int x;
        x = 1;
        
        if (conditions[0])
            x = 62;
            
        System.out.println(x);
    }
    
    // x: [1, 62]
    public static void TestMethod11(boolean... conditions) {
        int x;
        x = 1;
        
        if (conditions[0])
            if (conditions[1]) {
                if (conditions[2])
                    if (conditions[3]) {
                        x = 62;
                    }
            }
            
        System.out.println(x);
    }
    
    // x: [1, 9]
    // y: [62, 8]
    // z: [73, 7]
    public static void TestMethod12(boolean... conditions) {
        int x, y, z;
        x = 1;
        if (conditions[0]) {
            x = 9;
        }
        
        y = 62;
        if (conditions[1]) {
            y = 8;
        }
        
        z = 73;
        if (conditions[2]) {
            z = 7;
        }
        
        System.out.println(x);
    }
    
    // x: [1, 9]
    // y: [1, 9]
    public static void TestMethod13(boolean... conditions) {
        int x, y;
        x = 1;
        if (conditions[0]) {
            x = 9;
        }
        
        y = x;
        
        System.out.println(x);
    }
    
    // x: [10]
    // y: [1, 9]
    public static void TestMethod14(boolean... conditions) {
        int x, y;
        x = 1;
        if (conditions[0]) {
            x = 9;
        }
        
        y = x;
        x = 10;
        
        System.out.println(x);
    }
    
    // x: [1, 9, 10]
    // y: [1, 9]
    public static void TestMethod15(boolean... conditions) {
        int x, y;
        x = 1;
        if (conditions[0]) {
            x = 9;
        }
        
        y = x;
        
        if (conditions[1])
            x = 10;
        
        System.out.println(x);
    }
    
    // x: [10]
    public static void TestMethod16(boolean... conditions) {
        int x, y;
        x = 1;
        x = 2;
        x = 3;
        x = 4;
        x = 5;
        x = 6;
        x = 7;
        x = 8;
        x = 9;
        x = 10;
        
        System.out.println(x);
    }
    
    // Values are unknown
    public static void TestMethod17(boolean... conditions) {
        int x, y;
        
        System.out.println(x);
    }
    
    // x: [10]
    // y: [10]
    public static void TestMethod18(boolean... conditions) {
        int x, y;
        
        x = 10;
        y = x;
        
        System.out.println(x);
    }
    
    // x: [62]
    // y: [10]
    public static void TestMethod19(boolean... conditions) {
        int x, y;
        
        x = 10;
        y = x;
        x = 62;
        
        System.out.println(x);
    }
    
    // x: [10]
    // y: [62]
    public static void TestMethod20(boolean... conditions) {
        int x, y;
        
        x = 10;
        y = x;
        y = 62;
        
        System.out.println(x);
    }
    
    // No values -> no output
    public static void TestMethod21(boolean... conditions) {
    }
    
    // No values, empty if statements
    public static void TestMethod22(boolean... conditions) {
        if (conditions[0]);
        
        if (conditions[0]) { }
    }
    
    // x: [0, 3]
    public static void TestMethod23(boolean... conditions) {
        int x;
        x = 0;
        
        if (conditions[0])
        {
            x = 1;
            x = 2;
            x = 3;
        }
        
        System.out.println(x);
    } 
}