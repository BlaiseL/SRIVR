
public class Test {

	public int add(int x,int m){
		for (int y=0; y<m; y++)
			x++;
		return x;
	}
public static void main(String[] args){
	int[][] c= new int[3][];
	c[0]=new int[3];
	c[1]=new int [4];
	c[2]= new int[6];
	for (int x=0; x<c.length; x++){
		for (int y=0; y<c[x].length; y++){
			System.out.println(c[x][y]);
		}
		System.out.println("---------------------");
	}
	
	String str="abcdef";
	System.out.println(str.substring(0,str.length()-1));
	Test n=new Test();
	n.add(5, 7);
}
}